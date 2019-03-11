using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using POC_SignalR.App_Start;

[assembly: OwinStartup(typeof(Startup))]
namespace POC_SignalR.App_Start
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

            
            //ConfigurationOAuth(app);
            //var container = AutofacConfig.CreateContainer();

            //GlobalHost.DependencyResolver = new AutofacDependencyResolver(container) as IDependencyResolver;
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //TODO:先暫時拿掉SignalR 使用 Redis 的設定 , 這裡會出現Exception
            //var redisConfig = new Redis();
            //if (redisConfig.IsRedis)
            //    GlobalHost.DependencyResolver.UseRedis(redisConfig.RedisUrl, redisConfig.RedisPort, redisConfig.RedisPw, "JH.K.SignalR");

            //GlobalHost.HubPipeline.AddModule(new ExceptionPipelineModule());

            //Socket 重連時間為120秒, 預設30秒. 
            GlobalHost.Configuration.DisconnectTimeout = TimeSpan.FromSeconds(120);
            GlobalHost.Configuration.ConnectionTimeout = TimeSpan.FromSeconds(110);
            GlobalHost.Configuration.KeepAlive = TimeSpan.FromSeconds(10);

            var hubConfiguration = new HubConfiguration
            {
                EnableJavaScriptProxies = true, EnableJSONP = true, EnableDetailedErrors = true
            };
            //hubConfiguration.Resolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container);

            //app.UseAutofacMiddleware(container);
            app.MapSignalR(hubConfiguration);
            app.RunSignalR(hubConfiguration);
            //對所有Hub啟用身份驗證
            //GlobalHost.HubPipeline.RequireAuthentication();
            //ConfigurationRedis();

        }
    }
}