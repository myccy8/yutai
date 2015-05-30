using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Yutai.Admin.Models;
using Yutai.Dao.Models;
using Yutai.IService;

namespace Yutai.Admin.Controllers
{
    public class MusicCategoryController : BaseControlle
    {
        private IConcertCategoryRepo concertCategoryRepo;
        public MusicCategoryController(IConcertCategoryRepo concertCategoryRepo)
        {
            this.concertCategoryRepo = concertCategoryRepo;
        }
        [HttpPost]
        public Object GetList()
        {
            var category = concertCategoryRepo.GetCategory();
            if (category != null)
            {
                return category
                .Select(x => new { CategoryId = x.ConcertCategoryId, Name = x.Name }).ToList();
            }
            return string.Empty;
        }
        [HttpPost]
        public HttpResponseMessage Delete(BaseRequest request)
        {
            return base.getResponse(concertCategoryRepo.Del(request.Id));
        }
        [HttpPost]
        public HttpResponseMessage Add(ConcertCategory entity)
        {
            return base.getResponse(concertCategoryRepo.SaveCategory(entity));
        }
        [HttpPost]
        public object GetSingle(BaseRequest request)
        {
            var category = concertCategoryRepo.GetSingle(request.Id);
            return new { name = category.Name, id = category.ConcertCategoryId };
        }
        [HttpPost]
        public HttpResponseMessage Update(ConcertCategory entity)
        {
            return base.getResponse(concertCategoryRepo.Update(entity.ConcertCategoryId, entity.Name));
        }
    }
}
