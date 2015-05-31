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
    public class MusicController : BaseControlle
    {
        private IConcertRepo concertRepo;
        public MusicController(IConcertRepo concertRepo)
        {
            this.concertRepo = concertRepo;
        }
        [HttpPost]
        public object GetList(PageRequest request)
        {
            int total = 0;
            var list = concertRepo.GetConcerts(request.Id, request.PageIndex, request.PageSize, out total);
            return new
            {
                Data = list.Select(x => new
                {
                    ConcertId = x.ConcertId,
                    CategoryImage = x.CategoryImage,
                    Title = x.Title,
                    Like = x.Like,
                    Hate = x.Hate
                }),
                Total = total
            };
        }
        [HttpPost]
        public object GetSingle(BaseRequest request)
        {
            var item = concertRepo.GetSingle(request.Id);
            if (item != null)
            {
                return new
                {
                    Title = item.Title,
                    CategoryImage = item.CategoryImage,
                    ContentImage = item.ContentImage,
                    ConcertCategoryId = item.ConcertCategoryId,
                    Price = item.Price,
                    Lng = item.Lng,
                    Lat = item.Lat,
                    Detail = item.Detail,
                    Time = item.Time
                };
            }
            return null;
        }
        [HttpPost]
        public HttpResponseMessage Delete(BaseRequest request)
        {
            return base.getResponse(concertRepo.Del(request.Id));
        }
        [HttpPost]
        public HttpResponseMessage SaveItems()
        {

            var httpRequest = HttpContext.Current.Request;
            string uploadPath = HttpContext.Current.Server.MapPath("~/Images/Concert/");
            string fileName1 = Guid.NewGuid().ToString();
            string fileName2 = Guid.NewGuid().ToString();
            try
            {
                if (System.IO.Directory.Exists(uploadPath))
                {
                    if (httpRequest.Files.Count > 0)
                    {
                        var file1 = httpRequest.Files["Img1"];
                        var file2 = httpRequest.Files["Img2"];
                        string path1 = uploadPath + fileName1 + GetExtension(file1.FileName);
                        string path2 = uploadPath + fileName2 + GetExtension(file2.FileName);
                        if (!string.IsNullOrWhiteSpace(file1.FileName) && !string.IsNullOrWhiteSpace(file2.FileName))
                        {
                            file1.SaveAs(path1);
                            file2.SaveAs(path2);
                            Concert items = new Concert()
                            {
                                ConcertCategoryId = Convert.ToInt32(httpRequest.Form["categoryId"]),
                                CategoryImage = "/Images/Concert/" + fileName1 + GetExtension(file1.FileName),
                                ContentImage = "/Images/Concert/" + fileName2 + GetExtension(file2.FileName),
                                Title = httpRequest.Form["title"],
                                Lat = httpRequest.Form["lat"],
                                Address = httpRequest.Form["address"],
                                Lng = httpRequest.Form["lng"],
                                Time = httpRequest.Form["time"],
                                Detail = httpRequest.Form["detail"],
                                Price = httpRequest.Form["price"]
                            };
                            return base.getResponse(concertRepo.SaveConcert(items));
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
        public HttpResponseMessage UpdateItems()
        {
            var httpRequest = HttpContext.Current.Request;
            string uploadPath = HttpContext.Current.Server.MapPath("~/Images/Concert/");
            string fileName1 = Guid.NewGuid().ToString();
            string fileName2 = Guid.NewGuid().ToString();
            try
            {
                if (System.IO.Directory.Exists(uploadPath))
                {
                    if (httpRequest.Files.Count > 0)
                    {
                        var file1 = httpRequest.Files["Img1"];
                        var file2 = httpRequest.Files["Img2"];
                        string path1 = uploadPath + fileName1 + GetExtension(file1.FileName);
                        string path2 = uploadPath + fileName2 + GetExtension(file2.FileName);
                        Concert items = new Concert()
                        {
                            ConcertId = Convert.ToInt32(httpRequest.Form["concertId"]),
                            ConcertCategoryId = Convert.ToInt32(httpRequest.Form["categoryId"]),
                            Title = httpRequest.Form["title"],
                            Lat = httpRequest.Form["lat"],
                            Address = httpRequest.Form["address"],
                            Lng = httpRequest.Form["lng"],
                            Time = httpRequest.Form["time"],
                            Detail = httpRequest.Form["detail"],
                            Price = httpRequest.Form["price"]
                        };
                        if (!string.IsNullOrWhiteSpace(file1.FileName))
                        {
                            file1.SaveAs(path1);
                            items.CategoryImage = "/Images/Concert/" + fileName1 + GetExtension(file1.FileName);
                        }
                        if (!string.IsNullOrWhiteSpace(file2.FileName))
                        {
                            file2.SaveAs(path2);
                            items.ContentImage = "/Images/Concert/" + fileName2 + GetExtension(file2.FileName);
                        }
                        return base.getResponse(concertRepo.Update(items));
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
