using UnityEngine;

public class UnitStatUIPanel : MonoBehaviour
{
    [SerializeField] private UnitStatUI _playerStatUI;
    public UnitStatUI PlayerStatUI => _playerStatUI;

    [SerializeField] private UnitStatUI _EnemyStatUI;
    public UnitStatUI EnemyStatUI => _EnemyStatUI;

    public void InitializeUnitStatUI(Units unit)
    {
        if(unit is Player)
        {
            PlayerStatUI.SetStatus(unit.GetTotalStatData());
            PlayerStatUI.SetAPUI(unit.ActionPoint);
        }
        else
        {
            EnemyStatUI.SetStatus(unit.GetTotalStatData());
            EnemyStatUI.SetAPUI(unit.ActionPoint);
        }
    }

    public void UpdateUnitStatUI(Units playerUnit, Units EnemyUnit)
    {
        PlayerStatUI.SetStatus(playerUnit.GetTotalStatData());
        PlayerStatUI.SetAPUI(playerUnit.ActionPoint);
        EnemyStatUI.SetStatus(EnemyUnit.GetTotalStatData());
        EnemyStatUI.SetAPUI(EnemyUnit.ActionPoint);
    }
}
