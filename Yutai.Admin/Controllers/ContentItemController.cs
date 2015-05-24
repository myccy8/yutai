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
    public class ContentItemController : BaseControlle
    {
        private IContentItemRepo contentItemRepo;
        public ContentItemController(IContentItemRepo contentItemRepo)
        {
            this.contentItemRepo = contentItemRepo;
        }
        [HttpPost]
        public object GetList(PageRequest request)
        {
            int total = 0;
            var list = contentItemRepo.GetContentItems(request.Id, request.PageIndex, request.PageSize, out total);
            return new
            {
                Data = list.Select(x => new
                {
                    ContentItemId = x.ContentItemId,
                    ItemImage = x.ItemImage,
                    ItemTitle = x.ItemTitle,
                    ItemDetail = x.ItemDetail
                }),
                Total = total
            };
        }

        [HttpPost]
        public object GetSingle(BaseRequest request)
        {
            var item = contentItemRepo.GetSingle(request.Id);
            if (item != null)
            {
                return new
                {
                    ItemTitle = item.ItemTitle,
                    ItemDetail = item.ItemDetail,
                    ItemImage = item.ItemImage,
                    Url = item.Url
                };
            }
            return null;
        }
        [HttpPost]
        public HttpResponseMessage Delete(BaseRequest request)
        {
            return base.getResponse(contentItemRepo.Del(request.Id));
        }
        [HttpPost]
        public HttpResponseMessage SaveItems()
        {

            var httpRequest = HttpContext.Current.Request;
            string uploadPath = HttpContext.Current.Server.MapPath("~/Images/ContentItems/");
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
                            ContentItem items = new ContentItem()
                            {
                                CategoryItemsId = Convert.ToInt32(httpRequest.Form["categoryItemsId"]),
                                ItemImage = "/Images/ContentItems/" + fileName + GetExtension(file.FileName),
                                ItemTitle = httpRequest.Form["title"],
                                ItemDetail = httpRequest.Form["content"],
                                Url = httpRequest.Form["url"]
                            };
                            return base.getResponse(contentItemRepo.SaveContentItem(items));
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
            string uploadPath = HttpContext.Current.Server.MapPath("~/Images/ContentItems/");
            string fileName = Guid.NewGuid().ToString();
            try
            {
                if (System.IO.Directory.Exists(uploadPath))
                {
                    if (httpRequest.Files.Count > 0)
                    {
                        var file = httpRequest.Files["Img"];
                        string path = uploadPath + fileName + GetExtension(file.FileName);
                        ContentItem items = new ContentItem()
                        {
                            ContentItemId = Convert.ToInt32(httpRequest.Form["contentItemId"]),
                            CategoryItemsId = Convert.ToInt32(httpRequest.Form["categoryItemsId"]),
                            ItemTitle = httpRequest.Form["title"],
                            ItemDetail = httpRequest.Form["content"],
                            Url = httpRequest.Form["url"]
                        };
                        if (!string.IsNullOrWhiteSpace(file.FileName))
                        {
                            file.SaveAs(path);
                            items.ItemImage = "/Images/ContentItems/" + fileName + GetExtension(file.FileName);
                        }
                        return base.getResponse(contentItemRepo.Update(items));
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
