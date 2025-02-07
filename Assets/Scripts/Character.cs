using UnityEngine;

[RequireComponent(typeof(CharacterStatesSwitcher))]
public class Character : MonoBehaviour
{
    [SerializeField] private StatesConfig _statesConfig;

    private StateMachine _stateMachine;
    private StatesFactory _statesFactory;
    private CharacterStatesSwitcher _statesSwitcher;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _statesFactory = new StatesFactory();

        for (int i = 0; i < _statesConfig.States.Count; i++)
        {
            State state = _statesFactory.CreateState(_statesConfig.States[i]);
            
            if(i == 0)
            {
                _stateMachine = new StateMachine(state);
                _stateMachine.AddState(_statesConfig.States[i], state);
            }
            else
            {
                _stateMachine.AddState(_statesConfig.States[i], state);
            }

        }

        _statesSwitcher = GetComponent<CharacterStatesSwitcher>();
        _statesSwitcher.Init(_stateMachine);
    }
}
