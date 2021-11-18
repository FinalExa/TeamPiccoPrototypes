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
        playerRb.velocity = new Vector3(movementWithDirection.x, movementWithDirection.y, movementWithDirection.z) * playerController.actualSpeed;
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
    #endregion
}
