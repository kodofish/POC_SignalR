using System;

namespace POC_SignalR.Hub
{
    public class ChatMessage
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}