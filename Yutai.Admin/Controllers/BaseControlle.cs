using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Yutai.Admin.Controllers
{
    public class BaseControlle : ApiController
    {
        public HttpResponseMessage getResponse(object response)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            HttpContent content = new StringContent(Convert.ToString(response));
            responseMessage.Content = content;
            return responseMessage;
        }
        public string GetExtension(string fileName)
        {
            //int _Index = fileName.LastIndexOf(".");
            //string Extension = fileName.Substring(_Index);
            return ".png";
        }
        public void DeleteImg(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}