using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Dao.Models;
using Yutai.IService;

namespace Yutai.Service
{
    public class CategoryItemsRepo : BaseRepo, ICategoryItemsRepo
    {
        public List<CategoryItems> GetCategoryItems()
        {
            List<CategoryItems> entityList = null;
            Exec((db) =>
            {
                entityList = db.CategoryItems.ToList();
            });
            return entityList;
        }

        public List<Dao.Models.CategoryItems> GetCategoryItems(int categoryId, int pageIndex, int pageSize, out int total)
        {
            List<CategoryItems> entityList = null;
            int _total = 0;
            Exec((db) =>
            {
                var list = db.CategoryItems.Where(x => x.CategoryId == categoryId).ToList();
                if (pageSize == 0)
                {
                    entityList = list;
                }
                else
                {
                    entityList = list.Skip((pageIndex <= 0 ? 0 : pageIndex) * pageSize).Take(pageSize).ToList();
                }
                _total = list.Count;

            });
            total = _total;
            return entityList;
        }


        public bool SaveCategoryItems(Dao.Models.CategoryItems entity)
        {
            return Exec((db) =>
            {
                db.CategoryItems.Add(entity);
            }, true);
        }

        public bool Del(int id)
        {
            return Exec((db) =>
            {
                var entity = db.CategoryItems.SingleOrDefault(x => x.CategoryItemsId == id);
                if (entity != null)
                {
                    db.CategoryItems.Remove(entity);
                }
            }, true);
        }

        public bool Update(CategoryItems entity)
        {
            return Exec((db) =>
            {
                var updateEntity = db.CategoryItems.SingleOrDefault(x => x.CategoryItemsId == entity.CategoryItemsId);
                if (updateEntity != null)
                {
                    updateEntity.Content = entity.Content;
                    updateEntity.Title = entity.Title;
                    updateEntity.SecondTitle = entity.SecondTitle;
                    updateEntity.CategoryId = entity.CategoryId;
                    if (!string.IsNullOrWhiteSpace(entity.CategoryImage))
                    {
                        updateEntity.CategoryImage = entity.CategoryImage;
                    }
                    if (!string.IsNullOrWhiteSpace(entity.ContentImage))
                    {
                        updateEntity.ContentImage = entity.ContentImage;
                    }
                }
            }, true);
        }

        public Dao.Models.CategoryItems GetSingle(int id)
        {
            CategoryItems entity = null;
            Exec((db) =>
            {
                entity = db.CategoryItems.SingleOrDefault(x => x.CategoryItemsId == id);
            });
            return entity;
        }


        public CategoryItems GetSingleByName(string categoryItemName)
        {
            CategoryItems entity = null;
            Exec((db) =>
            {
                entity = db.CategoryItems.SingleOrDefault(x => x.Title == categoryItemName);
            });
            return entity;
        }
    }
}
