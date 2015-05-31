using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Dao.Models
{
     public  class CategoryItems
    {
         public int CategoryItemsId { get; set; }
         public int CategoryId { get; set; }
         public string CategoryImage { get; set; }
         public string ContentImage { get; set; }
         public string Title { get; set; }
         public string SecondTitle { get; set; }
         public string Content { get; set; }
         public virtual Category Category { get; set; }
         public virtual ICollection<ContentItem> ContentItem { get; set; }
    }
}
