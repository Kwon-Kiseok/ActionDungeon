using UnityEngine;
using UniRx;
using Zenject;

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
        if(_player is null)
        {
            _player = _gameInitializer.Player;
            _player.SetActionPosition(_playerActionPosition);
            _unitStatUIPanel.InitializeUnitStatUI(_player);

            _player.OnActionEnd.Subscribe((_) =>
            {
                _unitStatUIPanel.UpdateUnitStatUI(_player, _currentEnemy);
            });
        }

        if(_currentEnemy is null)
        {
            MatchingEnemy(_enemySpawner?.SpawnNewEnemy("Enemy/Enemy_Goblin", _enemySpawnPosition));
        }

        ButtonsEventAllocate();
    }

    private void ButtonsEventAllocate()
    {
        attackActionBtn.OnClickEvent += AttackActionEvent;
        defenceActionBtn.OnClickEvent += DefenceActionEvent;
        dodgeActionBtn.OnClickEvent += DodgeActionEvent;
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

    private void AttackActionEvent()
    {
        _player.CharacterController.DoAttackAction(_player, _currentEnemy, _player.GetStatData().luk);
        _currentEnemy.CharacterController.RandomAction(_currentEnemy, _player, _currentEnemy.GetStatData().luk);
    }
    
    private void DefenceActionEvent()
    {
        _player.CharacterController.DoDefenceAction(_player, _player.GetStatData().luk);
        _currentEnemy.CharacterController.RandomAction(_currentEnemy, _player, _currentEnemy.GetStatData().luk);
    }

    private void DodgeActionEvent()
    {
        _player.CharacterController.DoDodgeAction(_player, _player.GetStatData().luk);
        _currentEnemy.CharacterController.RandomAction(_currentEnemy, _player, _currentEnemy.GetStatData().luk);
    }
}
