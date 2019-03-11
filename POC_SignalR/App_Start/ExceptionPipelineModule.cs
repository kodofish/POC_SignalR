using Microsoft.AspNet.SignalR.Hubs;

namespace POC_SignalR
{
    /// <summary>
    ///     實作 Signalr HubPipelineModule, 用以補捉 Signalr 所產生的Exception
    /// </summary>
    /// <remarks>
    ///     請參考官方文件 https://docs.microsoft.com/zh-tw/aspnet/signalr/overview/guide-to-the-api/hubs-api-guide-server#how-to-handle-errors-in-the-hub-class
    /// </remarks>
    /// <seealso cref="Microsoft.AspNet.SignalR.Hubs.HubPipelineModule" />
    public class ExceptionPipelineModule : HubPipelineModule
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionPipelineModule"/> class.
        /// </summary>
        /// <param name="ravenClient">The raven client.</param>
        public ExceptionPipelineModule()
        {
        }

        /// <inheritdoc />
        protected override void OnIncomingError(ExceptionContext exceptionContext, IHubIncomingInvokerContext invokerContext)
        {
            dynamic caller = invokerContext.Hub.Clients.Caller;
            caller.ExceptionHandler(exceptionContext.Error.Message);
            base.OnIncomingError(exceptionContext, invokerContext);
        }


    }
}