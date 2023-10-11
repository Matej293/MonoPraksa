using System.ComponentModel;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Example.Model;
using Example.Model.Common;
using Example.Repository;
using Example.Repository.Common;
using Example.Service;
using Example.Service.Common;
using Example.WebApi.Controllers;

namespace Example.WebApi.App_Start
{
    public static class AutofacConfig
    {
        public static Autofac.IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<CityController>();
            builder.RegisterType<RandomSubclassController>();

            builder.RegisterType<CityService>().As<ICityService>();
            builder.RegisterType<RandomSubclassService>().As<IRandomSubclassService>();

            builder.RegisterType<CityRepository>().As<ICityRepository>();
            builder.RegisterType<RandomSubclassRepository>().As<IRandomSubclassRepository>();

            builder.RegisterType<CityModel>().As<ICityModel>();
            builder.RegisterType<RandomSubclassModel>().As<RandomSubclassModel>();

            var container = builder.Build();

            return container;            
        }
    }
}