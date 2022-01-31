using UnityEngine;
public abstract class StateMachine : MonoBehaviour
{
    protected State _state;

    public virtual void SetState(State state)
    {
        _state = state;
        state.Start();
    }
    public virtual void Start()
    {
        _state.Start();
    }
    private void Update()
    {
        _state.StateUpdate();
    }
}
