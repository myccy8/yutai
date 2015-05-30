using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Dao.Models
{
   public class Concert
    {

       public int ConcertId { get; set; }
       public int ConcertCategoryId { get; set; }
        public string CategoryImage { get; set; }
        public string ContentImage { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Time { get; set; }
        public string Price { get; set; }
        public string Detail { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public int Like { get; set; }
        public int Hate{ get; set; }
        public virtual ConcertCategory ConcertCategory { get; set; }
    }
}
