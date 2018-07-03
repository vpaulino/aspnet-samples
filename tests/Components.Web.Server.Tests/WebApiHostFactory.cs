using System;
using System.Collections.Generic;
using System.Text;

#if NETCORE
using Components.Web.Server.Tests.Owin;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace Components.Web.Server.Tests
{
    public class WebApiHostFactory<TStartup> : WebApplicationFactory<Startup> 
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                //builder.UseSolutionRelativeContentRoot("../");
                builder.UseContentRoot("./");
            });
        }


        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder(new string[0]).UseStartup<Startup>();
        }


    }
}
#endif