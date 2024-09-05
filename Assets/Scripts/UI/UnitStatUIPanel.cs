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
            PlayerStatUI.SetStatus(unit.GetStatData());
            PlayerStatUI.SetAPUI(unit.ActionPoint);
        }
        else
        {
            EnemyStatUI.SetStatus(unit.GetStatData());
            EnemyStatUI.SetAPUI(unit.ActionPoint);
        }
    }

    public void UpdateUnitStatUI(Units playerUnit, Units EnemyUnit)
    {
        PlayerStatUI.SetStatus(playerUnit.GetStatData());
        PlayerStatUI.SetAPUI(playerUnit.ActionPoint);
        EnemyStatUI.SetStatus(EnemyUnit.GetStatData());
        EnemyStatUI.SetAPUI(EnemyUnit.ActionPoint);
    }
}
