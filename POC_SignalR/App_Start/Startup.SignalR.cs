using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using POC_SignalR;

[assembly: OwinStartup(typeof(Startup))]
namespace POC_SignalR
{
    public partial class Startup
    {
        public void ConfigurationSignalR(IAppBuilder app)
        {
            GlobalHost.HubPipeline.AddModule(new ExceptionPipelineModule());
            
            // Configuration SignalR Scale out
            //GlobalHost.DependencyResolver.UseStackExchangeRedis("127.0.0.1", 6379, "", "POC_SignalR");

            var hubConfiguration = new HubConfiguration
            {
                EnableJavaScriptProxies = true,
                EnableJSONP = true,
                EnableDetailedErrors = true
            };

            app.MapSignalR(hubConfiguration);
            app.RunSignalR(hubConfiguration);

            //對所有Hub啟用身份驗證
            //GlobalHost.HubPipeline.RequireAuthentication();

        }
    }
}