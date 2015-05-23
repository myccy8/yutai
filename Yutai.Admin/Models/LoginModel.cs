using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yutai.Admin.Models
{
    public class LoginModel
    {
        public class LoginRequest
        {
            public string LoginName { get; set; }
            public string Password { get; set; }
            public bool isRemember { set; get; }
        }
    }
}