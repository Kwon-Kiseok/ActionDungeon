public class StateMachine
{
    public IState CurrentState { get; private set; }

    private readonly Units _units;

    public StateMachine(Units units)
    {
        this._units = units;
    }

    public void SetState(IState state)
    {
        if(CurrentState == state)
        {
            return;
        }

        CurrentState?.ExitState();
        CurrentState = state;
        CurrentState.ChangeUnit(this._units);
    }

    // Update?
    public void DoExecuteState()
    {
        if(CurrentState is not null)
        {
            CurrentState.ExecuteState();
        }
    }
}
