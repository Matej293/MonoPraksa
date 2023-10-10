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

namespace Example.WebApi.App_Start
{
    public static class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());


            builder.RegisterType<CityService>().As<ICityService>();
            builder.RegisterType<RandomSubclassService>().As<IRandomSubclassService>();

            builder.RegisterType<CityRepository>().As<ICityRepository>();
            builder.RegisterType<RandomSubclassRepository>().As<IRandomSubclassRepository>();

            builder.RegisterType<CityModel>().As<ICityModel>();
            builder.RegisterType<RandomSubclassModel>().As<IRandomSubclassModel>();


            var container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}