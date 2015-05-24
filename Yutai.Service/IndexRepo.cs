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
    public class IndexRepo :BaseRepo,IIndexRepo
    {
        public List<HomeEntity> GetHomeImage()
        {
            List<HomeEntity> entityList = null; 
            Exec((db) => {
                entityList = db.HomeEntity.ToList();           
            });
            return entityList;
        }

        public bool SaveHomeImage(HomeEntity entity)
        {
         return  Exec((db) =>
            {
                db.HomeEntity.Add(entity);
            },true);
        }

        public bool DelHomeImage(int id)
        {
            return Exec((db) =>
            {
                var entity = db.HomeEntity.SingleOrDefault(x => x.Id == id);
                if (entity != null)
                {
                    db.HomeEntity.Remove(entity);
                }
            },true);
        }

        public bool UpdateHomeImage(HomeEntity entity)
        {
           return  Exec((db) =>
            {
                db.Set<HomeEntity>().Attach(entity);
                db.Entry<HomeEntity>(entity).State = EntityState.Modified;
            },true);
        }


        public HomeEntity GetSingle(int id)
        {

            HomeEntity entity= null;
            Exec((db) =>
            {
                entity= db.HomeEntity.SingleOrDefault(x=>x.Id==id);
            });
            return entity;
        }
    }
}
