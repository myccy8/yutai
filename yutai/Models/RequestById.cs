using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace yutai.Models
{
    public class RequestById
    {
        public int id { get; set; }
        public int categoryId { get; set; }
        public int index { get; set; }
        public int size { get; set; }
    }
}