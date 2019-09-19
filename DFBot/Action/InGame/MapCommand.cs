namespace DFBot.Action.InGame
{
    public class MapCommand : ICommand
    {
        private Bot _bot;

        public MapCommand(Bot bot = null)
        {
            _bot = bot;
        }

        public void Execute(string message)
        {
            _bot.InGame(message);
        }
    }
}
