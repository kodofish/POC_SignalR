namespace POC_SignalR.Hub
{
    public sealed class MainHub : Microsoft.AspNet.SignalR.Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        /// <summary>
        ///     Sends the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="message">The message.</param>
        public void Send(string name, string message)
        {
            Clients.All.broadcastMessage(name, message);
        }
    }
}