using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Http.Validation;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Owin;
using WebApiHost.Controllers;
using Components.Web.Http.Validation;

namespace WebApiHost
{
    public class Startup
    {
        string Resource = "Adapter";
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.

        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
 
            ConfigureFormatters(config);
            ConfiguringRoutes(this.Resource, config);
            ConfigureTracing(config);
            ConfigureModelValidation(config);
            
            appBuilder.UseWebApi(config);
        }

        public static  void ConfigureModelValidation(HttpConfiguration config)
        {
            config.Services.Replace(typeof(IBodyModelValidator), new PolicyBasedObjectModelValidator(new InheritanceModelValidationPolicy(typeof(object))));
        }

        public static void ConfigureTracing(HttpConfiguration config)
        {
            config.EnableSystemDiagnosticsTracing();
        }

        public static void ConfigureFormatters(HttpConfiguration config)
        {
            
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
              //  Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
            };

            settings.Converters.Add(new StringEnumConverter { CamelCaseText = false });
          
            config.Formatters.JsonFormatter.SerializerSettings = settings;

        }
        

        public static void ConfiguringRoutes(string resource, HttpConfiguration configuration)
        {

            var controllerName = "Process";

            configuration.Routes.MapHttpRoute(
                name: $"{resource}{nameof(ProcessController.GetSubject)}",
                routeTemplate: "{id}",
                defaults: new { controller = controllerName, action = nameof(ProcessController.GetSubject) },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            configuration.Routes.MapHttpRoute(
                name: $"{resource}{nameof(ProcessController.List)}",
                routeTemplate: "",
                defaults: new { controller = controllerName, action = nameof(ProcessController.List) },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            configuration.Routes.MapHttpRoute(
                name: $"{resource}{nameof(ProcessController.DeleteSubject)}",
                routeTemplate: "{id}",
                defaults: new { controller = controllerName, action = nameof(ProcessController.DeleteSubject) },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Delete) });

            configuration.Routes.MapHttpRoute(
                name: $"{resource}{nameof(ProcessController.Update)}",
                routeTemplate: "{id}",
                defaults: new { controller = controllerName, action = nameof(ProcessController.Update) },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) });

            configuration.Routes.MapHttpRoute(
                name: $"{resource}{nameof(ProcessController.Enroll)}",
                routeTemplate: "",
                defaults: new { controller = controllerName, action = nameof(ProcessController.Enroll) },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            configuration.Routes.MapHttpRoute(
                name: $"{resource}{nameof(ProcessController.Process)}",
                routeTemplate: "Process",
                defaults: new { controller = controllerName, action = nameof(ProcessController.Process) },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            configuration.Routes.MapHttpRoute(
                name: $"{resource}{nameof(ProcessController.Verify)}",
                routeTemplate: "Verify",
                defaults: new { controller = controllerName, action = nameof(ProcessController.Verify) },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });
            configuration.Routes.MapHttpRoute(
                name: $"{resource}{nameof(ProcessController.Clear)}",
                routeTemplate: "Clear",
                defaults: new { controller = controllerName, action = nameof(ProcessController.Clear) },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });
            configuration.Routes.MapHttpRoute(
                name: $"{resource}{nameof(ProcessController.Identify)}",
                routeTemplate: "Identify",
                defaults: new { controller = controllerName, action = nameof(ProcessController.Identify) },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });
            configuration.Routes.MapHttpRoute(
                name: $"{resource}{nameof(ProcessController.Match)}",
                routeTemplate: "Match",
                defaults: new { controller = controllerName, action = nameof(ProcessController.Match) },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });
        }
    }
}
