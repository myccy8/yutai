using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Yutai.Admin.Models;
using Yutai.Dao.Models;
using Yutai.IService;

namespace Yutai.Admin.Controllers
{
    public class IndexController : BaseControlle
    {
        private IIndexRepo indexRepo;
        public IndexController(IIndexRepo indexRepo)
        {
            this.indexRepo = indexRepo;
        }
        [HttpPost]
        public HttpResponseMessage SaveImage()
        {
            var httpRequest = HttpContext.Current.Request;
            string uploadPath = HttpContext.Current.Server.MapPath("~/Images/Home/");
            string fileName = Guid.NewGuid().ToString();
            try
            {
                if (System.IO.Directory.Exists(uploadPath))
                {
                    if (httpRequest.Files.Count > 0)
                    {
                        var file = httpRequest.Files["Img"];
                        string path = uploadPath + fileName + GetExtension(file.FileName);
                        if (!string.IsNullOrWhiteSpace(file.FileName))
                        {
                            file.SaveAs(path);
                            HomeEntity home = new HomeEntity()
                            {
                                Content = httpRequest.Form["content"],
                                ImagePath = "/Images/Home/" + fileName + GetExtension(file.FileName),
                                Url = httpRequest.Form["url"],
                                Title = httpRequest.Form["title"]
                            };
                            indexRepo.SaveHomeImage(home);
                            return base.getResponse(true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
              
            }
            return base.getResponse(false);
        }
        [HttpPost]
        public List<HomeEntity> GetList()
        {
            return indexRepo.GetHomeImage();
        }
        [HttpPost]
        public HttpResponseMessage DeleteImage(BaseRequest request)
        {
            return base.getResponse(indexRepo.DelHomeImage(request.Id));
        }
        [HttpPost]
        public HomeEntity GetSingle(BaseRequest request)
        {
            return indexRepo.GetSingle(request.Id);
        }
        [HttpPost]
        public HttpResponseMessage UpdateImage()
        {
            var httpRequest = HttpContext.Current.Request;
            string uploadPath = HttpContext.Current.Server.MapPath("~/Images/Home/");
            string fileName = Guid.NewGuid().ToString();
            try
            {
                if (System.IO.Directory.Exists(uploadPath))
                {
                    if (httpRequest.Files.Count > 0)
                    {
                        var file = httpRequest.Files["Img"];
                        int id = Convert.ToInt32(httpRequest.Form["id"]);
                        HomeEntity home = new HomeEntity()
                        {
                            Id = id,
                            Content = httpRequest.Form["content"],
                            Url = httpRequest.Form["url"],
                            ImagePath = httpRequest.Form["imageUrl"],
                            Title = httpRequest.Form["title"]
                        };
                        if (!string.IsNullOrWhiteSpace(file.FileName))
                        {
                            string path = uploadPath + fileName + GetExtension(file.FileName);
                            file.SaveAs(path);
                            home.ImagePath = "/Images/Home/" + fileName + GetExtension(file.FileName);
                        }
                      
                        bool b = indexRepo.UpdateHomeImage(home);
                        return base.getResponse(b);
                    }
                }
            }
            catch (Exception ex)
            {
              
            }

            return base.getResponse(false);
        }
    }
}
