using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Dao.Models;

namespace Yutai.IService
{
     public  interface IContentItemRepo
    {
         List<ContentItem> GetContentItems();
         List<ContentItem> GetContentItems(int categoryItemId, int pageIndex, int pageSize, out int total);
         bool SaveContentItem(ContentItem entity);
        bool Del(int id);
        bool Update(ContentItem entity);
        ContentItem GetSingle(int id);
    }
}
