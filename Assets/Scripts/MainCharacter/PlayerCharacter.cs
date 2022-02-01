using UnityEngine;
public class PlayerCharacter : StateMachine
{
    [HideInInspector] public PlayerController playerController;
    [HideInInspector] public string thisStateName;

    public override void SetState(State state)
    {
        base.SetState(state);
        thisStateName = state.ToString();
        playerController.curState = thisStateName;
    }

    private void Awake()
    {
        playerController = this.gameObject.GetComponent<PlayerController>();
        SetState(new Idle(this));
    }

    private void OnCollisionStay(Collision collision)
    {
        _state.Collisions(collision);
    }
}
