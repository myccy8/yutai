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
    public class CategoryController : BaseControlle
    {
        private ICategoryRepo categoryRepo;
        public CategoryController(ICategoryRepo categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }
        [HttpPost]
        public Object GetList()
        {
            var category = categoryRepo.GetCategory();
            if (category != null)
            {
                return category
                .Select(x => new { CategoryId = x.CategoryId, Name = x.Name }).ToList();
            }
            return string.Empty;
        }
        [HttpPost]
        public HttpResponseMessage Delete(BaseRequest request)
        {
            return base.getResponse(categoryRepo.Del(request.Id));
        }
        [HttpPost]
        public HttpResponseMessage Add(Category entity)
        {
            return base.getResponse(categoryRepo.SaveCategory(entity));
        }
        [HttpPost]
        public object GetSingle(BaseRequest request)
        {
            var category = categoryRepo.GetSingle(request.Id);
            return new { name = category.Name, id = category.CategoryId };
        }
        [HttpPost]
        public HttpResponseMessage Update(Category entity)
        {
            return base.getResponse(categoryRepo.Update(entity.CategoryId, entity.Name));
        }
    }
}
