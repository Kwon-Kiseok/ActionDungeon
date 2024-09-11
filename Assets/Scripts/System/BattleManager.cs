using UnityEngine;
using UniRx;
using Zenject;
using UniRx.Triggers;

public class BattleManager : MonoBehaviour
{
    // �÷��̾�� �� ��Ī
    // �÷��̾�� �� ���� ���μ��� ����
    // �÷��̾�� ���� �ൿ ������� ���� ���� ����
    // ��� �Ǻ�

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

    // ������ Update�� ��ü�Ͽ� ����� UniRx�� ������Ʈ
    // ��� ���� -> ������ Monobehaviour�� ������Ʈ���� ������ ������ ����
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
            // enemyKey �ܰ迡 �°� ����
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
                // ��Ʋ ��� ����
                // �÷��̾� ���� ���� �� ���� ����
                // ���� ��Ʋ �� ���ο� �� ����
            }
            else if (!_player.IsAlive)
            {
                Debug.Log("Player Lose TT");
                _isBattleEnd = true;
                // ���� ����
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
