using UnityEngine;
using Zenject;

public class UnitStatUIPanel : MonoBehaviour
{
    [SerializeField] private UnitStatUI _playerStatUI;
    public UnitStatUI PlayerStatUI => _playerStatUI;

    [SerializeField] private UnitStatUI _EnemyStatUI;
    public UnitStatUI EnemyStatUI => _EnemyStatUI;

    private TurnClockSystem _turnClockSystem;

    [Inject]
    public void Inject(TurnClockSystem turnClockSystem)
    {
        _turnClockSystem = turnClockSystem;
    }

    public void InitializeUnitStatUI(Units unit)
    {
        if(unit is Player)
        {
            PlayerStatUI.SetStatus(unit.GetTotalStatData(), !_turnClockSystem.IsDays);
            PlayerStatUI.SetAPUI(unit.ActionPoint);
        }
        else
        {
            EnemyStatUI.SetStatus(unit.GetTotalStatData(), _turnClockSystem.IsDays);
            EnemyStatUI.SetAPUI(unit.ActionPoint);
        }
    }

    public void UpdateUnitStatUI(Units playerUnit, Units EnemyUnit)
    {
        PlayerStatUI.SetStatus(playerUnit.GetTotalStatData(), !_turnClockSystem.IsDays);
        PlayerStatUI.SetAPUI(playerUnit.ActionPoint);
        EnemyStatUI.SetStatus(EnemyUnit.GetTotalStatData(), _turnClockSystem.IsDays);
        EnemyStatUI.SetAPUI(EnemyUnit.ActionPoint);
    }
}
