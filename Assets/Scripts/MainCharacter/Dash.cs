using UnityEngine;

public class Dash : PlayerState
{
    private bool dashFinished;
    private float dashTimer;
    private Vector3 dashVector;
    public Dash(PlayerCharacter playerCharacter) : base(playerCharacter)
    {
    }

    public override void Start()
    {
        if (!_playerCharacter.playerController.DashLocked)
        {
            DashSetup();
        }
        else Transitions();
    }
    public override void StateUpdate()
    {
        if (!dashFinished) PerformDash();
    }
    public override void Collisions(Collision collision)
    {
        //if (!collision.gameObject.CompareTag("Ground")) EndDash();
    }

    #region Dash
    private void DashSetup()
    {
        PlayerData playerData = _playerCharacter.playerController.playerReferences.playerData;
        dashFinished = false;
        float speed = playerData.dashDistance / playerData.dashDuration;
        Vector3 forward = DashNormalization();
        dashVector = forward * speed;
        dashTimer = playerData.dashDuration;
    }

    private Vector3 DashNormalization()
    {
        Camera mainCamera = _playerCharacter.playerController.playerReferences.mainCamera;
        Vector3 forward = mainCamera.transform.forward;
        forward.y = 0f;
        Vector3 right = mainCamera.transform.right;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        return (_playerCharacter.playerController.lastDashDirection.x * forward) + (_playerCharacter.playerController.lastDashDirection.z * right);
    }

    private void PerformDash()
    {
        if (dashTimer > 0 && !_playerCharacter.playerController.DashLocked)
        {
            Rigidbody playerRb = _playerCharacter.playerController.playerReferences.playerRb;
            dashTimer -= Time.deltaTime;
            playerRb.velocity = dashVector;
        }
        else
        {
            EndDash();
        }
    }
    private void EndDash()
    {
        dashTimer = _playerCharacter.playerController.playerReferences.playerData.dashDuration;
        Rigidbody playerRb = _playerCharacter.playerController.playerReferences.playerRb;
        playerRb.velocity = Vector3.zero;
        dashFinished = true;
        _playerCharacter.playerController.DashLocked = true;
        Transitions();
    }
    #endregion

    #region Transitions
    private void Transitions()
    {
        PlayerInputs playerInputs = _playerCharacter.playerController.playerReferences.playerInputs;
        if (playerInputs.MovementInput == Vector3.zero) ReturnToIdle();
        else ReturnToMovement();
    }
    private void ReturnToIdle()
    {
        _playerCharacter.SetState(new Idle(_playerCharacter));
    }
    private void ReturnToMovement()
    {
        _playerCharacter.SetState(new Moving(_playerCharacter));
    }
    #endregion
}
