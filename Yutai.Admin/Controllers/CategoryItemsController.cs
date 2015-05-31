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
    public class CategoryItemsController : BaseControlle
    {
        private ICategoryItemsRepo categoryItemsRepo;
        public CategoryItemsController(ICategoryItemsRepo categoryItemsRepo)
        {
            this.categoryItemsRepo = categoryItemsRepo;
        }
        [HttpPost]
        public object GetList(PageRequest request)
        {
            int total = 0;
            var list = categoryItemsRepo.GetCategoryItems(request.Id, request.PageIndex, request.PageSize, out total);
            return new
            {
                Data = list.Select(x => new
                {
                    CategoryItemsId = x.CategoryItemsId,
                    CategoryImage = x.CategoryImage,
                    ContentImage = x.ContentImage,
                    Content = x.Content,
                    Title = x.Title
                }),
                Total = total
            };
        }
        [HttpPost]
        public object GetAllList()
        {
            var list = categoryItemsRepo.GetCategoryItems();
            if (list != null)
            {
                return new
                {
                    Data = list.Select(x => new
                    {
                        CategoryItemsId = x.CategoryItemsId,
                        Title = x.Title
                    })
                };
            }
            return string.Empty;
        }
        [HttpPost]
        public object GetSingle(BaseRequest request)
        {
            var item = categoryItemsRepo.GetSingle(request.Id);
            if (item != null)
            {
                return new
                {
                    Title = item.Title,
                    SecondTitle=item.SecondTitle,
                    Content = item.Content,
                    CategoryImage = item.CategoryImage,
                    ContentImage = item.ContentImage,
                    CategoryItemsId = item.CategoryItemsId
                };
            }
            return null;
        }
        [HttpPost]
        public HttpResponseMessage Delete(BaseRequest request)
        {
            return base.getResponse(categoryItemsRepo.Del(request.Id));
        }
        [HttpPost]
        public HttpResponseMessage SaveItems()
        {

            var httpRequest = HttpContext.Current.Request;
            string uploadPath = HttpContext.Current.Server.MapPath("~/Images/CategoryItems/");
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
                            CategoryItems items = new CategoryItems()
                            {
                                CategoryId = Convert.ToInt32(httpRequest.Form["categoryId"]),
                                CategoryImage = "/Images/CategoryItems/" + fileName1 + GetExtension(file1.FileName),
                                ContentImage = "/Images/CategoryItems/" + fileName2 + GetExtension(file2.FileName),
                                Title = httpRequest.Form["title"],
                                SecondTitle = httpRequest.Form["title2"],
                                Content = httpRequest.Form["content"]
                            };
                            return base.getResponse(categoryItemsRepo.SaveCategoryItems(items));
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
            string uploadPath = HttpContext.Current.Server.MapPath("~/Images/CategoryItems/");
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
                        CategoryItems items = new CategoryItems()
                        {
                            CategoryItemsId = Convert.ToInt32(httpRequest.Form["categoryitemsid"]),
                            CategoryId = Convert.ToInt32(httpRequest.Form["category"]),
                            Title = httpRequest.Form["title"],
                            SecondTitle = httpRequest.Form["title2"],
                            Content = httpRequest.Form["content"]
                        };
                        if (!string.IsNullOrWhiteSpace(file1.FileName))
                        {
                            file1.SaveAs(path1);
                            items.CategoryImage = "/Images/CategoryItems/" + fileName1 + GetExtension(file1.FileName);
                        }
                        if (!string.IsNullOrWhiteSpace(file2.FileName))
                        {
                            file2.SaveAs(path2);
                            items.ContentImage = "/Images/CategoryItems/" + fileName2 + GetExtension(file2.FileName);
                        }
                        return base.getResponse(categoryItemsRepo.Update(items));
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
