namespace EventArch
{
    public static class Events
    {
        public static OnStartGame onStartGame = new();
        public static OnFinishGame onFinishGame = new();
    }

    public class OnStartGame : GameEvent
    {
    }

    public class OnFinishGame : GameEvent
    {
        public bool WinState { get; set; }
    }
}