using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Security;
using Yutai.Admin.Authorize;
using Yutai.Admin.Models;

namespace Yutai.Admin.Controllers
{
    public class LoginController : ApiController
    {
        [System.Web.Http.ActionName("PostLoginUser")]
        public HttpResponseMessage PostLoginUser(LoginModel.LoginRequest request)
        {

            HttpResponseMessage respMessage = new HttpResponseMessage();
            string encTicket = "";
            string username = ConfigurationManager.AppSettings["UserName"].ToString();
            string password = ConfigurationManager.AppSettings["Password"].ToString();
            bool loginResult = false;
            if (request.LoginName == username && request.Password == password)
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                                                                                         username,
                                                                                         DateTime.Now,
                                                                                         DateTime.Now.AddMinutes(24 * 60),
                                                                                         false,
                                                                                         username,
                                                                                         FormsAuthentication
                                                                                             .FormsCookiePath);
                encTicket = FormsAuthentication.Encrypt(ticket);
                var cookie = new CookieHeaderValue(FormsAuthentication.FormsCookieName, encTicket);
                if (request.isRemember)
                {

                    cookie.Expires = DateTime.Now.AddDays(7);
                }
                cookie.Path = "/";
                respMessage.Headers.AddCookies(new CookieHeaderValue[] { cookie });
                loginResult = true;
            }

            HttpContent content = new StringContent(loginResult.ToString());
            //content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            respMessage.Content = content;
            return respMessage;
        }

        [System.Web.Http.ActionName("GetLoginName")]
        [HttpAuthorize]
        public string PostGetLoginName()
        {
            CookieHeaderValue value = Request.Headers.GetCookies(FormsAuthentication.FormsCookieName).FirstOrDefault();
            if (value != null)
            {
                string encTicket = value[FormsAuthentication.FormsCookieName].Value;
                string loginname = FormsAuthentication.Decrypt(encTicket).UserData;
                return loginname;
            }
            else
            {
                return string.Empty;
            }
        }
        [System.Web.Http.ActionName("LogOut")]
        public void PostLogOut()
        {
            FormsAuthentication.SignOut();
        }

    }
}
