using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Dao.Models
{
    public class HomeEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string ImagePath { get; set; }
        public string Url { get; set; }
        public string Content { get; set; }
    }
}
