namespace DFBot.Action.ConnectChar
{
    /// <summary>
    /// Manage the connection of the account
    /// </summary>
    public class ConnectionCommand : ICommand
    {
        private readonly Bot _bot;

        public ConnectionCommand(Bot bot)
        {
            _bot = bot;
        }

        public void Execute(string message)
        {
            _bot.Connection(message);
        }
    }
}
