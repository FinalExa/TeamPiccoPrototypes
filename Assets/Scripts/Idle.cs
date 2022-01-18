public class Idle : PlayerState
{
    public Idle(PlayerCharacter playerCharacter) : base(playerCharacter)
    {
    }

    public override void Start()
    {
        PlayerReferences playerReferences = _playerCharacter.playerController.playerReferences;
    }

    public override void StateUpdate()
    {
        Transitions();
    }

    #region Transitions
    private void Transitions()
    {
        PlayerInputs playerInputs = _playerCharacter.playerController.playerReferences.playerInputs;
        GoToMovementState(playerInputs);
        GoToDashState(playerInputs);
    }
    #region ToMovementState
    private void GoToMovementState(PlayerInputs playerInputs)
    {
        if ((playerInputs.MovementInput != UnityEngine.Vector3.zero)) _playerCharacter.SetState(new Moving(_playerCharacter));
    }
    #endregion
    #region ToDashState
    private void GoToDashState(PlayerInputs playerInputs)
    {
        if ((playerInputs.DashInput)) _playerCharacter.SetState(new Dash(_playerCharacter));
    }
    #endregion
    #endregion
}
