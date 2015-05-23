using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Security;
using System.Net.Http;

namespace Yutai.Admin.Authorize
{
    public class HttpAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //  Uri uri = actionContext.Request.RequestUri;
            //  string strurl = uri.PathAndQuery;
            CookieHeaderValue cookie = actionContext.Request.Headers.GetCookies(FormsAuthentication.FormsCookieName).FirstOrDefault();
            // CookieHeaderValue cookiepara = actionContext.Request.Headers.GetCookies("pageurl").FirstOrDefault();
            if (cookie == null)
            {
                HandleUnauthorizedRequest(actionContext);
            }
            else
            {

                //if (!strurl.Equals(""))
                //{
                //    string ModuleName = "";
                //    if (dicModule.TryGetValue(strurl, out ModuleName))
                //    {
                //        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie[FormsAuthentication.FormsCookieName].Value);
                //        if (ticket != null)
                //        {
                //            if (ModuleName == "菜单")
                //            {

                //            }
                //            else
                //            {
                //                IMenuBLL bll = new MenuBLL();
                //                IList<LoadModuleByLoginNameResult> list = bll.LoadMenuModle(2, ticket.UserData);
                //                if (list.Where(s => s.ModuleName == ModuleName).ToList().Count > 0)
                //                {
                //                    //返回200
                //                    //var challengeMessage = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK);
                //                    //actionContext.Response = challengeMessage;
                //                }
                //                else
                //                {
                //                    HandleUnauthorizedRequest(actionContext);
                //                }

                //            }

                //        }
                //        else
                //        {
                //            HandleUnauthorizedRequest(actionContext);
                //        }
                //    }
                //    else
                //    {
                //        HandleUnauthorizedRequest(actionContext);
                //    }

                //}
            }
        }

        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //返回无授权
            var challengeMessage = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            actionContext.Response = challengeMessage;
        }
    }
}