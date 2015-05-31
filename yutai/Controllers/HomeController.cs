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
        private IConcertCategoryRepo concertCategoryRepo;
        private IConcertRepo concertRepo;
        public HomeController(IIndexRepo indexRepo,
            ICategoryRepo categoryRepo,
            IContentItemRepo contentItemRepo,
            ICategoryItemsRepo categoryItemsRepo,
            IConcertCategoryRepo concertCategoryRepo,
            IConcertRepo concertRepo)
        {
            this.indexRepo = indexRepo;
            this.categoryRepo = categoryRepo;
            this.categoryItemsRepo = categoryItemsRepo;
            this.contentItemRepo = contentItemRepo;
            this.concertCategoryRepo = concertCategoryRepo;
            this.concertRepo = concertRepo;
        }
        [Route("homeImage")]
        [HttpGet]
        public List<HomeEntity> GetHomeImage()
        {
            return indexRepo.GetHomeImage("0");
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
                    title = x.Title,
                    secondTitle=x.SecondTitle
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
                obj = new
                {
                    title = item.Title,
                    indexImage = item.ContentImage,
                    categoryImage = item.CategoryImage,
                    content = item.Content,
                    secondTitle=item.SecondTitle,
                    categoryName = item.Title,
                    total = total,
                    articles = articles.Select(x => new { detail = x.ItemDetail, title = x.ItemTitle, image = x.ItemImage, url = x.Url })
                };
            }
            return obj;
        }

        [Route("getmusiclist")]
        [HttpPost]
        public object GetMusicList(RequestById request)
        {
            int total = 0;
            var list = concertRepo.GetConcerts(Convert.ToInt32(request.id), request.index, request.size, out total);
            var data=list.Select(x => new { id= x.ConcertId,title = x.Title, time = x.Time, address = x.Address, price = x.Price, image = x.CategoryImage }).ToList();
            return new { data = data, total = total };
        }
        [Route("setmusiclike")]
        [HttpPost]
        public object SetMusicLike(Music request)
        {
            return concertRepo.SetStatus(request.type, request.id);
        }

        [Route("getmusiccategory")]
        [HttpGet]
        public object GetMusicCategory()
        {
            var res = concertCategoryRepo.GetCategory();
            var data= res.Select(x => new { concertCategoryId = x.ConcertCategoryId, name = x.Name }).ToList();
            return new { category = data, images = indexRepo.GetHomeImage("1") };
        }
        [Route("getmusic/{id}")]
        [HttpGet]
        public object GetMusic(int id)
        {
            var res = concertRepo.GetSingle(id);
            return new
            {
                id=res.ConcertId,
                address = res.Address,
                title = res.Title,
                time = res.Time,
                price = res.Price,
                lng = res.Lng,
                lat = res.Lat,
                like = res.Like,
                hate = res.Hate,
                detail = res.Detail,
                contentImage = res.ContentImage
            };
        }
    }
}
