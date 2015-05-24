using Microsoft.Owin.Cors;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Cors;
namespace yutai
{
    public class CorsSetting
    {
        public static CorsOptions SetCors() 
        {
            var tokenCorsPolicy = new CorsPolicy { AllowAnyHeader = true, AllowAnyMethod = true, AllowAnyOrigin = true };
            var corsOptions = new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = (request) =>
                    {
                        var requestHeaders = (IDictionary<string, string[]>)request.Environment["owin.RequestHeaders"];
                        string Origin = requestHeaders["Origin"][0];
                        ArrayList CorsHostList = new ArrayList(System.Configuration.ConfigurationManager.AppSettings["CorsHost"].Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries));
                        return Task.FromResult(CorsHostList.Contains(Origin) ? tokenCorsPolicy : null);
                    }

                }
            };
            return corsOptions;
        }
    }
}