using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Dao.Models
{
    public class ContentItem
    {
        public int ContentItemId { get; set; }
        public int CategoryItemsId { get; set; }
        public string ItemImage { get; set; }
        public string ItemTitle { get; set; }
        public string ItemDetail { get; set; }
        public string Url { get; set; }
        public virtual CategoryItems CategoryItems { get; set; }
    }
}
