namespace Game.Scripts.GameLogic
{
    public interface IGameStateObserver
    {
        void OnGameStateUpdated(GameManager.GameState state);
    }
}