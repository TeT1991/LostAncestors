public class StateMachine 
{
    private EntityState _currentState;

    public EntityState CurrentState => _currentState;

    public void Init(EntityState startingState)
    {
        _currentState = startingState;
        _currentState.Enter();
    }

    public void ChangeState(EntityState newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}
