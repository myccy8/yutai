using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Yutai.IService;

namespace yutai.Controllers
{
    [RoutePrefix("api")]
    public class MessageController: ApiController
    {
        public MessageController(IClass1 class1)
        {
            this.class1 = class1;
        }
        private IClass1 class1 { get; set; }
        [Route("Greeting")]
        [HttpGet]
        public Greeting Getss()
        {
            return new Greeting
            {
                Text = class1.getName()
            };
        }
    }
    public class Greeting
    {
        public string Text { get; set; }
    }
}