using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Dao.Models;
using Yutai.IService;

namespace Yutai.Service
{
    public class CategoryRepo : BaseRepo, ICategoryRepo
    {
        public List<Dao.Models.Category> GetCategory()
        {
            List<Category> entityList = null;
            Exec((db) =>
            {
                entityList = db.Category.ToList();
            });
            return entityList;
        }

        public bool SaveCategory(Dao.Models.Category entity)
        {
            return Exec((db) =>
            {
                db.Category.Add(entity);
            }, true);
        }

        public bool Del(int id)
        {
            return Exec((db) =>
            {
                var entity = db.Category.SingleOrDefault(x => x.CategoryId == id);
                if (entity != null)
                {
                    db.Category.Remove(entity);
                }
            }, true);
        }

        public bool Update(int id, string name)
        {
            return Exec((db) =>
            {
                var updateEntity = db.Category.SingleOrDefault(x => x.CategoryId == id);
                if (updateEntity != null)
                {
                    updateEntity.Name = name;
                }
            }, true);
        }

        public Dao.Models.Category GetSingle(int id)
        {

            Category entity = null;
            Exec((db) =>
            {
                entity = db.Category.SingleOrDefault(x => x.CategoryId == id);
            });
            return entity;
        }


        public Category GetSingleByName(string name)
        {

            Category entity = null;
            Exec((db) =>
            {
                entity = db.Category.SingleOrDefault(x => x.Name == name);
            });
            return entity;
        }
    }
}
