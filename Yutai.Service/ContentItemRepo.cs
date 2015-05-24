using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Dao.Models;
using Yutai.IService;

namespace Yutai.Service
{
   public class ContentItemRepo : BaseRepo, IContentItemRepo
    {
        public List<ContentItem> GetContentItems()
        {
            List<ContentItem> entityList = null;
            Exec((db) =>
            {
                entityList = db.ContentItem.ToList();
            });
            return entityList;
        }

        public List<Dao.Models.ContentItem> GetContentItems(int categoryItemId, int pageIndex, int pageSize, out int total)
        {
            List<ContentItem> entityList = null;
            int _total = 0;
            Exec((db) =>
            {
                var list = db.ContentItem.Where(x => x.CategoryItemsId == categoryItemId).ToList();
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

        public bool SaveContentItem(Dao.Models.ContentItem entity)
        {
            return Exec((db) =>
            {
                db.ContentItem.Add(entity);
            }, true);
        }

        public bool Del(int id)
        {
            return Exec((db) =>
            {
                var entity = db.ContentItem.SingleOrDefault(x => x.ContentItemId == id);
                if (entity != null)
                {
                    db.ContentItem.Remove(entity);
                }
            }, true);
        }

        public bool Update(ContentItem entity)
        {
            return Exec((db) =>
            {
                var updateEntity = db.ContentItem.SingleOrDefault(x => x.ContentItemId == entity.ContentItemId);
                if (updateEntity != null)
                {
                    updateEntity.CategoryItemsId = entity.CategoryItemsId;
                    updateEntity.ItemDetail = entity.ItemDetail;
                    updateEntity.ItemTitle = entity.ItemTitle;
                    updateEntity.Url = entity.Url;
                    if (!string.IsNullOrWhiteSpace(entity.ItemImage))
                    {
                        updateEntity.ItemImage = entity.ItemImage;
                    }
                }
            }, true);
        }

        public Dao.Models.ContentItem GetSingle(int id)
        {
            ContentItem entity = null;
            Exec((db) =>
            {
                entity = db.ContentItem.SingleOrDefault(x => x.ContentItemId == id);
            });
            return entity;
        }
    }
}
