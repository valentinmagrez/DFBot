namespace DFBot.Action
{
    /// <summary>
    /// Using the command pattern
    /// Command interface
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Execute the specific action
        /// associated to the command
        /// </summary>
        /// <param name="message">message to communicate to the client</param>
        void Execute(string message);
    }
}
