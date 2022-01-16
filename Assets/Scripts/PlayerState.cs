public class PlayerState : State
{
    protected PlayerCharacter _playerCharacter;
    public PlayerState(PlayerCharacter playerCharacter)
    {
        _playerCharacter = playerCharacter;
    }
}
