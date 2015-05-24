using Autofac;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Autofac.Integration.WebApi;
using Yutai.IService;
using Yutai.Service;
using System.Data.Entity;
using Yutai.Dao;
namespace yutai
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Database.SetInitializer<YutaiDB>(null);
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            app.UseCors(CorsSetting.SetCors())
                  .UseAutofacMiddleware(RegisterApi())
                  .UseAutofacWebApi(config)
                  .UseWebApi(config);
        }
        public IContainer RegisterApi()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).InstancePerRequest();
            builder.RegisterType<IndexRepo>().As<IIndexRepo>();
            builder.RegisterType<CategoryRepo>().As<ICategoryRepo>();
            builder.RegisterType<CategoryItemsRepo>().As<ICategoryItemsRepo>();
            builder.RegisterType<ContentItemRepo>().As<IContentItemRepo>();
            return builder.Build();
        }
    }
}