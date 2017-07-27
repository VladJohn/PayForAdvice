using FluentValidation.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPI.Packages;
using WebAPI.Validators;

namespace WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            var corsAtr = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsAtr);
            config.MapHttpAttributeRoutes();

            // Web API configuration and services


           // config.Filters.Add(new ValidateModelStateFilter());
           // config.MessageHandlers.Add(new ResponseWrappingHandler());



            // Web API routes
          //  config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            FluentValidationModelValidatorProvider.Configure(config);
        }
    }
}
