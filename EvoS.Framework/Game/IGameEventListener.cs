namespace EvoS.Framework.Game
{
    public interface IGameEventListener
    {
        void OnGameEvent(GameEventManager.EventType eventType, GameEventManager.GameEventArgs args);
    }
}
