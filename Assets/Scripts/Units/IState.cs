using UnityEngine;

public interface IState
{
    public void ChangeUnit(Units unit);
    public void EnterState();
    public void ExecuteState();
    public void ExitState();
}
