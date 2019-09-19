namespace DFBot.Action.ConnectChar
{
    /// <summary>
    /// Manage the selection of the character
    /// </summary>
    public class SelectCharacterCommand : ICommand
    {
        private Bot _bot;

        public SelectCharacterCommand(Bot bot)
        {
            _bot = bot;
        }

        public void Execute(string message)
        {
            _bot.PersoSelection(message);
        }
    }
}
