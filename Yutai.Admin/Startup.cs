using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using Yutai.Service;
using Yutai.IService;

[assembly: OwinStartup(typeof(Yutai.Admin.Startup))]

namespace Yutai.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
 
        }
    }
}
