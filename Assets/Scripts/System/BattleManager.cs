using UnityEngine;
using UniRx;
using Zenject;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;

public class BattleManager : MonoBehaviour
{
    public enum BattleState
    {
        READY,
        PROGRESS,
        END
    }

    private Player _player;
    private Enemy _currentEnemy;

    private EnemySpawner _enemySpawner;
    private GameInitializer _gameInitializer;
    private TurnClockSystem _turnClockSystem;
    private BattleReadyUI _battleReadyUI;
    private GameProgressUI _gameProgressUI;
    private CameraController _cameraController;

    private BattleState _battleState = BattleState.READY;
    public BattleState BState => _battleState;

    [SerializeField] private Transform _enemySpawnPosition;
    [SerializeField] private Transform _playerActionPosition;
    [SerializeField] private Transform _enemyActionPosition;

    [Header("Unit Stat UIs")]
    [SerializeField] private UnitStatUIPanel _unitStatUIPanel;

    [Header("Action Buttons")]
    [SerializeField] private ActionButton attackActionBtn;
    [SerializeField] private ActionButton defenceActionBtn;
    [SerializeField] private ActionButton dodgeActionBtn;

    [SerializeField] private ActionEnhanceBonus actionEnhanceBonus;
    [SerializeField] private BattleResultUI battleResultUI;

    public Subject<Unit> OnActionSubject = new Subject<Unit>();

    [Inject]
    public void Inject(GameInitializer gameInitializer, EnemySpawner enemySpawner, TurnClockSystem turnClockSystem, 
        BattleReadyUI battleReadyUI, GameProgressUI gameProgressUI, CameraController cameraController)
    {
        _gameInitializer = gameInitializer;
        _enemySpawner = enemySpawner;
        _turnClockSystem = turnClockSystem;
        _battleReadyUI = battleReadyUI;
        _gameProgressUI = gameProgressUI;
        _cameraController = cameraController;
    }

    public void Start()
    {
        InitPlayer();
        MatchingNewEnemy();
        UniRxUpdate();
        ButtonsEventAllocate();
        BattleResultEvent();
    }

    private void UniRxUpdate()
    {
        this.UpdateAsObservable().Subscribe((_) =>
        {
            CheckBattleEnd();
        }).AddTo(this);
    }

    private void ButtonsEventAllocate()
    {
        attackActionBtn.OnClickEvent += AttackActionEvent;
        defenceActionBtn.OnClickEvent += DefenceActionEvent;
        dodgeActionBtn.OnClickEvent += DodgeActionEvent;
    }

    private void InitPlayer()
    {
        if (_player is null)
        {
            _player = _gameInitializer.Player;
            _player.SetActionPosition(_playerActionPosition);
            _unitStatUIPanel.InitializeUnitStatUI(_player);
            actionEnhanceBonus.SetPlayer(_player);

            _player.OnHitSubject.Subscribe((_) => {
                _cameraController.SetHitActionFocusCamera();
            }).AddTo(this);

            _player.OnActionEnd.Subscribe((_) =>
            {
                _unitStatUIPanel.UpdateUnitStatUI(_player, _currentEnemy);
            }).AddTo(this);

            _player.OnSelectBonusStatSubject.Subscribe((_) =>
            {
                _unitStatUIPanel.UpdateUnitStatUI(_player, _currentEnemy);
            }).AddTo(this);

            _player.ActionSuccessSubject.Subscribe((_) =>
            {
                actionEnhanceBonus.IncreaseSuccessCount();
            }).AddTo(this);
        }
    }

    private void BattleResultEvent()
    {
        battleResultUI.OnNextBattleSubject.Subscribe((_) =>
        {
            ReleaseCurrentEnemy();
            PrepareNextBattle();
        }).AddTo(this);
    }

    private void MatchingNewEnemy()
    {
        if (_currentEnemy is null)
        {
            _enemySpawner.InitSpawner();
            MatchingEnemy(_enemySpawner?.SpawnNewEnemy(_enemySpawnPosition));
            _battleReadyUI.SetEnemyInfoUI(_currentEnemy);
            _battleReadyUI.IntroduceNextEnemy();
        }
    }

    private void ReleaseCurrentEnemy()
    {
        Destroy(_enemySpawner.EnemyInstance);
        _currentEnemy = null;
    }

    private void MatchingEnemy(Enemy nextEnemy)
    {
        _currentEnemy = nextEnemy;
        _currentEnemy.SetActionPosition(_enemyActionPosition);
        _currentEnemy.SetUnitPosition(_enemySpawnPosition.position);

        _unitStatUIPanel.InitializeUnitStatUI(_currentEnemy);

        _currentEnemy.OnActionEnd.Subscribe((_) =>
        {
            _unitStatUIPanel.UpdateUnitStatUI(_player, _currentEnemy);
        }).AddTo(this);

        _currentEnemy.OnHitSubject.Subscribe((_) => {
            _cameraController.SetHitActionFocusCamera();
        }).AddTo(this);

        _battleState = BattleState.PROGRESS;
    }

    private void CheckBattleEnd()
    {
        if (BState != BattleState.PROGRESS)
        {
            return;
        }

        if (_player is not null && _currentEnemy is not null)
        {
            if (_player.IsAlive && !_currentEnemy.IsAlive)
            {
                Debug.Log("Player Win !!");
                _gameProgressUI.AddKilledEnemyCount(1);
                _battleState = BattleState.END;
                battleResultUI.BattleResultEnable(_player);
                actionEnhanceBonus.CloseDraftSystemUI();
            }
            else if (!_player.IsAlive)
            {
                Debug.Log("Player Lose TT");
                _battleState = BattleState.END;
                battleResultUI.BattleResultEnable(_player);
                actionEnhanceBonus.CloseDraftSystemUI();
            }
        }
    }

    private void PrepareNextBattle()
    {
        _battleState = BattleState.READY;
        MatchingNewEnemy();
    }

    private void AttackActionEvent()
    {
        if (BState != BattleState.PROGRESS)
        {
            return;
        }

        if (_turnClockSystem.IsDays)
        {
            _player.CharacterController.DoAttackAction(_player, _currentEnemy, _player.GetTotalStatData().luk);
            _currentEnemy.CharacterController.RandomAction(_currentEnemy, _player, _currentEnemy.GetTotalStatData().luk * 0.5f);
        }
        else
        {
            _player.CharacterController.DoAttackAction(_player, _currentEnemy, _player.GetTotalStatData().luk * 0.5f);
            _currentEnemy.CharacterController.RandomAction(_currentEnemy, _player, _currentEnemy.GetTotalStatData().luk);
        }

        OnActionSubject.OnNext(Unit.Default);
    }

    private void DefenceActionEvent()
    {
        if (BState != BattleState.PROGRESS)
        {
            return;
        }

        if (_turnClockSystem.IsDays)
        {
            _player.CharacterController.DoDefenceAction(_player, _player.GetTotalStatData().luk);
            _currentEnemy.CharacterController.RandomAction(_currentEnemy, _player, _currentEnemy.GetTotalStatData().luk * 0.5f);
        }
        else
        {
            _player.CharacterController.DoDefenceAction(_player, _player.GetTotalStatData().luk * 0.5f);
            _currentEnemy.CharacterController.RandomAction(_currentEnemy, _player, _currentEnemy.GetTotalStatData().luk);
        }

        OnActionSubject.OnNext(Unit.Default);
    }

    private void DodgeActionEvent()
    {
        if (BState != BattleState.PROGRESS)
        {
            return;
        }

        if (_turnClockSystem.IsDays)
        {
            _player.CharacterController.DoDodgeAction(_player, _player.GetTotalStatData().luk);
            _currentEnemy.CharacterController.RandomAction(_currentEnemy, _player, _currentEnemy.GetTotalStatData().luk * 0.5f);
        }
        else
        {
            _player.CharacterController.DoDodgeAction(_player, _player.GetTotalStatData().luk * 0.5f);
            _currentEnemy.CharacterController.RandomAction(_currentEnemy, _player, _currentEnemy.GetTotalStatData().luk);
        }

        OnActionSubject.OnNext(Unit.Default);
    }
}
