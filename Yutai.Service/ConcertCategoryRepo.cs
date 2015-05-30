using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Dao.Models;
using Yutai.IService;

namespace Yutai.Service
{
    public class ConcertCategoryRepo : BaseRepo, IConcertCategoryRepo
    {
        public List<Dao.Models.ConcertCategory> GetCategory()
        {
            List<ConcertCategory> entityList = null;
            Exec((db) =>
            {
                entityList = db.ConcertCategory.ToList();
            });
            return entityList;
        }

        public bool SaveCategory(Dao.Models.ConcertCategory entity)
        {
            return Exec((db) =>
            {
                db.ConcertCategory.Add(entity);
            }, true);
        }

        public bool Del(int id)
        {
            return Exec((db) =>
            {
                var entity = db.ConcertCategory.SingleOrDefault(x => x.ConcertCategoryId == id);
                if (entity != null)
                {
                    db.ConcertCategory.Remove(entity);
                }
            }, true);
        }

        public bool Update(int id, string name)
        {
            return Exec((db) =>
            {
                var updateEntity = db.ConcertCategory.SingleOrDefault(x => x.ConcertCategoryId == id);
                if (updateEntity != null)
                {
                    updateEntity.Name = name;
                }
            }, true);
        }

        public Dao.Models.ConcertCategory GetSingle(int id)
        {
            ConcertCategory entity = null;
            Exec((db) =>
            {
                entity = db.ConcertCategory.SingleOrDefault(x => x.ConcertCategoryId == id);
            });
            return entity;
        }
    }
}
