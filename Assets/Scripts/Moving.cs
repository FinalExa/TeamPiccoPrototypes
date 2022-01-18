using UnityEngine;
public class Moving : PlayerState
{
    public Moving(PlayerCharacter playerCharacter) : base(playerCharacter)
    {
    }
    public override void Start()
    {
        PlayerReferences playerReferences = _playerCharacter.playerController.playerReferences;

    }
    public override void StateUpdate()
    {
        Movement();
        Transitions();
        UpdateSpeedValue();
    }

    #region Movement
    private void UpdateSpeedValue()
    {
        PlayerData playerData = _playerCharacter.playerController.playerReferences.playerData;
        PlayerController playerController = _playerCharacter.playerController;
        playerController.actualSpeed = playerData.defaultMovementSpeed;
    }
    private void Movement()
    {
        Rigidbody playerRb = _playerCharacter.playerController.playerReferences.playerRb;
        PlayerController playerController = _playerCharacter.playerController;
        Vector3 movementWithDirection = MovementInitialization();
        playerRb.velocity = movementWithDirection * playerController.actualSpeed;
    }

    private Vector3 MovementInitialization()
    {
        Camera mainCamera = _playerCharacter.playerController.playerReferences.mainCamera;
        PlayerInputs playerInputs = _playerCharacter.playerController.playerReferences.playerInputs;
        Vector3 forward = mainCamera.transform.forward;
        forward.y = 0f;
        Vector3 right = mainCamera.transform.right;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        return (playerInputs.MovementInput.x * forward) + (playerInputs.MovementInput.z * right);
    }
    #endregion

    #region Transitions
    private void Transitions()
    {
        PlayerInputs playerInputs = _playerCharacter.playerController.playerReferences.playerInputs;
        GoToIdleState(playerInputs);
        GoToDashState(playerInputs);
    }
    #region ToIdleState
    private void GoToIdleState(PlayerInputs playerInputs)
    {
        if (playerInputs.MovementInput == Vector3.zero)
        {
            _playerCharacter.SetState(new Idle(_playerCharacter));
        }
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
