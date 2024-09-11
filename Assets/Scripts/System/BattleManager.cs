using UnityEngine;
using UniRx;
using Zenject;
using UniRx.Triggers;

public class BattleManager : MonoBehaviour
{
    // 플레이어와 적 매칭
    // 플레이어와 적 전투 프로세스 진행
    // 플레이어와 적의 행동 결과값에 따라서 판정 적용
    // 사망 판별

    private Player _player;
    private Enemy _currentEnemy;

    private EnemySpawner _enemySpawner;
    private GameInitializer _gameInitializer;

    private bool _isBattleEnd = false;
    
    [SerializeField] private Transform _enemySpawnPosition;
    [SerializeField] private Transform _playerActionPosition;
    [SerializeField] private Transform _enemyActionPosition;

    [Header("Unit Stat UIs")]
    [SerializeField] private UnitStatUIPanel _unitStatUIPanel;

    [Header("Action Buttons")]
    [SerializeField] private ActionButton attackActionBtn;
    [SerializeField] private ActionButton defenceActionBtn;
    [SerializeField] private ActionButton dodgeActionBtn;

    [Inject]
    public void Inject(GameInitializer gameInitializer, EnemySpawner enemySpawner)
    {
        _gameInitializer = gameInitializer;
        _enemySpawner = enemySpawner;
    }

    public void Start()
    {
        InitPlayer();
        InitEnemy();
        UniRxUpdate();
        ButtonsEventAllocate();
    }

    // 기존의 Update를 대체하여 사용할 UniRx의 업데이트
    // 사용 이유 -> 기존의 Monobehaviour의 업데이트보다 성능적 측면이 좋음
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

            _player.OnActionEnd.Subscribe((_) =>
            {
                _unitStatUIPanel.UpdateUnitStatUI(_player, _currentEnemy);
            });
        }
    }

    private void InitEnemy()
    {
        if (_currentEnemy is null)
        {
            MatchingEnemy(_enemySpawner?.SpawnNewEnemy("Enemy/Enemy_Goblin", _enemySpawnPosition));
        }
    }

    private void MatchingNewEnemy()
    {
        if (_currentEnemy is null)
        {
            // enemyKey 단계에 맞게 변경
            MatchingEnemy(_enemySpawner?.SpawnNewEnemy("Enemy/Enemy_Goblin", _enemySpawnPosition));
        }
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
        });
    }

    private void CheckBattleEnd()
    {
        if(_isBattleEnd)
        {
            return;
        }

        if (_player is not null && _currentEnemy is not null)
        {
            if (_player.IsAlive && !_currentEnemy.IsAlive)
            {
                Debug.Log("Player Win !!");
                _isBattleEnd = true;
                // 배틀 결과 정산
                // 플레이어 전투 보상 및 성장 선택
                // 다음 배틀 전 새로운 적 생성
            }
            else if (!_player.IsAlive)
            {
                Debug.Log("Player Lose TT");
                _isBattleEnd = true;
                // 게임 종료
            }
        }
    }

    private void AttackActionEvent()
    {
        if(_isBattleEnd)
        {
            return;
        }

        _player.CharacterController.DoAttackAction(_player, _currentEnemy, _player.GetStatData().luk);
        _currentEnemy.CharacterController.RandomAction(_currentEnemy, _player, _currentEnemy.GetStatData().luk);
    }
    
    private void DefenceActionEvent()
    {
        if (_isBattleEnd)
        {
            return;
        }

        _player.CharacterController.DoDefenceAction(_player, _player.GetStatData().luk);
        _currentEnemy.CharacterController.RandomAction(_currentEnemy, _player, _currentEnemy.GetStatData().luk);
    }

    private void DodgeActionEvent()
    {
        if (_isBattleEnd)
        {
            return;
        }

        _player.CharacterController.DoDodgeAction(_player, _player.GetStatData().luk);
        _currentEnemy.CharacterController.RandomAction(_currentEnemy, _player, _currentEnemy.GetStatData().luk);
    }
}
