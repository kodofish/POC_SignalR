using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace POC_SignalR.Hub
{
    public class MainHub : Microsoft.AspNet.SignalR.Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }

        #region "Client Event"
        /// <summary>
        /// Sends the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="message">The message.</param>
        public void Send(string name, string message)
        {
            Clients.All.broadcastMessage(name, message);
        }

        #endregion
    }
}