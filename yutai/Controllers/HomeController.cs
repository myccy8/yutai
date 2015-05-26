using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using yutai.Models;
using Yutai.Dao.Models;
using Yutai.IService;

namespace yutai.Controllers
{
    [RoutePrefix("api/home")]
    public class HomeController : ApiController
    {
        private IIndexRepo indexRepo;
        private ICategoryRepo categoryRepo;
        private ICategoryItemsRepo categoryItemsRepo;
        private IContentItemRepo contentItemRepo;
        public HomeController(IIndexRepo indexRepo,
            ICategoryRepo categoryRepo,
            IContentItemRepo contentItemRepo,
            ICategoryItemsRepo categoryItemsRepo)
        {
            this.indexRepo = indexRepo;
            this.categoryRepo = categoryRepo;
            this.categoryItemsRepo = categoryItemsRepo;
            this.contentItemRepo = contentItemRepo;
        }
        [Route("homeImage")]
        [HttpGet]
        public List<HomeEntity> GetHomeImage()
        {
            return indexRepo.GetHomeImage();
        }

        [Route("getcategoryItems")]
        [HttpPost]
        public object GetCategoryItems(BaseRequest request)
        {
            var category = categoryRepo.GetSingleByName(request.name);
            object list = null;
            int total = 0;
            if (category != null)
            {
                var item = categoryItemsRepo.GetCategoryItems(category.CategoryId, 0, 0, out total);
                list = item.Select(x => new
                {
                    categoryId = x.CategoryId,
                    categoryItemsId = x.CategoryItemsId,
                    categoryImage = x.CategoryImage,
                    title = x.Title
                });
            }
            return list;
        }
        [Route("getarticles")]
        [HttpPost]
        public object GetArticles(RequestById request)
        {
            int total = 0;
            object obj = new object();
            var item = categoryItemsRepo.GetSingleByName(request.categoryItemName);
            if (item != null)
            {
                var articles = contentItemRepo.GetContentItems(item.CategoryItemsId, request.index, request.size, out total);
                obj= new
                {
                    title = item.Title,
                    indexImage = item.ContentImage,
                    categoryImage = item.CategoryImage,
                    content = item.Content,
                    categoryName = item.Title,
                    total = total,
                    articles = articles.Select(x => new { detail = x.ItemDetail, title = x.ItemTitle, image = x.ItemImage, url = x.Url })
                };
            }
            return obj;
        }
    }
}
