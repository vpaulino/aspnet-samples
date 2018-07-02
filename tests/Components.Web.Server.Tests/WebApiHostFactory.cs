using System;
using System.Collections.Generic;
using System.Text;

#if NETCORE
using Components.Web.Server.Tests.Owin;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;


namespace Components.Web.Server.Tests
{
    public class WebApiHostFactory<TStartup> : WebApplicationFactory<Startup> 
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
        }


    }
}
#endif