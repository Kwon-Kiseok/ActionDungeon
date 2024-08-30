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
        }
        else
        {
            EnemyStatUI.SetStatus(unit.GetStatData());
        }
    }
}
