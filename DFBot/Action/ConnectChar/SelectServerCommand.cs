namespace DFBot.Action.ConnectChar
{
    /// <summary>
    /// Manage the selection of the server
    /// </summary>
    public class SelectServerCommand : ICommand
    {
        private readonly Bot _bot;

        public SelectServerCommand(Bot bot)
        {
            _bot = bot;
        }

        public void Execute(string message)
        {
           _bot.ServerSelection(message);
        }
    }
}
