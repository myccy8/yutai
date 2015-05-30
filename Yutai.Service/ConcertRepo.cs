using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Dao.Models;
using Yutai.IService;

namespace Yutai.Service
{
    public class ConcertRepo : BaseRepo, IConcertRepo
    {
        public List<Concert> GetConcerts()
        {
            List<Concert> entityList = null;
            Exec((db) =>
            {
                entityList = db.Concert.ToList();
            });
            return entityList;
        }

        public List<Dao.Models.Concert> GetConcerts(int categoryId, int pageIndex, int pageSize, out int total)
        {
            List<Concert> entityList = null;
            int _total = 0;
            Exec((db) =>
            {
                var list = db.Concert.Where(x => x.ConcertCategoryId == categoryId).ToList();
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

        public bool SaveConcert(Dao.Models.Concert entity)
        {
            return Exec((db) =>
            {
                db.Concert.Add(entity);
            }, true);
        }

        public bool Del(int id)
        {
            return Exec((db) =>
            {
                var entity = db.Concert.SingleOrDefault(x => x.ConcertId == id);
                if (entity != null)
                {
                    db.Concert.Remove(entity);
                }
            }, true);
        }

        public bool Update(Dao.Models.Concert entity)
        {
            return Exec((db) =>
            {
                var updateEntity = db.Concert.SingleOrDefault(x => x.ConcertId == entity.ConcertId);
                if (updateEntity != null)
                {
                    updateEntity.ConcertCategoryId = entity.ConcertCategoryId;
                    updateEntity.Content = entity.Content;
                    updateEntity.Detail = entity.Detail;
                    updateEntity.Lat = entity.Lat;
                    updateEntity.Lng = entity.Lng;
                    updateEntity.Time = entity.Time;
                    updateEntity.Price = entity.Price;
                    updateEntity.Title = entity.Title;
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

        public Dao.Models.Concert GetSingle(int id)
        {
            Concert entity = null;
            Exec((db) =>
            {
                entity = db.Concert.SingleOrDefault(x => x.ConcertId == id);
            });
            return entity;
        }


        public bool SetStatus(int type, int id)
        {
            return Exec((db) =>
            {
                var updateEntity = db.Concert.SingleOrDefault(x => x.ConcertId == id);
                if (updateEntity != null)
                {
                    if (type == 0)
                    {
                        updateEntity.Like++;
                    }
                    else if (type == 1)
                    {
                        updateEntity.Hate++;
                    }
                }
            }, true);
        }

    }
    }

