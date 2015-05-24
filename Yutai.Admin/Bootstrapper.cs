using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Autofac.Integration.WebApi;
using System.Reflection;
using Yutai.Service;
using Yutai.IService;

namespace Yutai.Admin
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacWebAPI();
        }
        private static void SetAutofacWebAPI()
        {
            var configuration = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();
            builder.RegisterType<IndexRepo>().As<IIndexRepo>();
            builder.RegisterType<CategoryRepo>().As<ICategoryRepo>();
            builder.RegisterType<CategoryItemsRepo>().As<ICategoryItemsRepo>();
            builder.RegisterType<ContentItemRepo>().As<IContentItemRepo>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(builder.Build());
        }
    }
}