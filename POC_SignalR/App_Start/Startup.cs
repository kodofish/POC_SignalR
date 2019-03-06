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
    public class Startup
    {
        /// <summary>
        ///     Configurations the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);

            //app.Map("/signalr", map =>
            //{
            //    // Setup the CORS middleware to run before SignalR.
            //    // By default this will allow all origins. You can
            //    // configure the set of origins and/or http verbs by
            //    // providing a cors options with a different policy.
            //    map.UseCors(CorsOptions.AllowAll);
            //    var hubConfiguration = new HubConfiguration
            //    {
            //        // You can enable JSONP by uncommenting line below.
            //        // JSONP requests are insecure but some older browsers (and some
            //        // versions of IE) require JSONP to work cross domain
            //        // EnableJSONP = true
            //    };
            //    // Run the SignalR pipeline. We're not using MapSignalR
            //    // since this branch already runs under the "/signalr"
            //    // path.

            //    hubConfiguration.EnableDetailedErrors = true;
            //    map.RunSignalR(hubConfiguration);
            //});

            //ConfigurationOAuth(app);
            var hubConfiguration = new HubConfiguration();
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

            //hubConfiguration.Resolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container);
            hubConfiguration.EnableJavaScriptProxies = true;
            hubConfiguration.EnableJSONP = true;
            hubConfiguration.EnableDetailedErrors = true;

            //app.UseAutofacMiddleware(container);
            app.MapSignalR(hubConfiguration);
            //app.RunSignalR(hubConfiguration);
            //對所有Hub啟用身份驗證
            //GlobalHost.HubPipeline.RequireAuthentication();
            //ConfigurationRedis();

            #region "Web API 2"
            //var httpConfig = GlobalConfiguration.Configuration;
            //httpConfig.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            //HttpConfiguration webApiConfiguration = new HttpConfiguration();
            //WebApiConfig.Register(webApiConfiguration);
            //app.UseAutofacWebApi(webApiConfiguration);
            //app.UseWebApi(webApiConfiguration);
            #endregion
        }
    }
}