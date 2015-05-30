using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Dao.Models
{
    public class ConcertCategory
    {
        public int ConcertCategoryId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Concert> Concerts { get; set; }
    }
}
