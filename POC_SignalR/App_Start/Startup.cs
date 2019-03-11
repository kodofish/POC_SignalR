using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using Startup = POC_SignalR.Startup;

[assembly: OwinStartup(typeof(Startup))]
namespace POC_SignalR
{
    public partial class Startup
    {
        /// <summary>
        ///     Configurations the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            ConfigurationRedis();
 

            //Socket 重連時間為120秒, 預設30秒. 
            GlobalHost.Configuration.DisconnectTimeout = TimeSpan.FromSeconds(120);
            GlobalHost.Configuration.ConnectionTimeout = TimeSpan.FromSeconds(110);
            GlobalHost.Configuration.KeepAlive = TimeSpan.FromSeconds(10);

            ConfigurationSignalR(app);
        }
    }
}