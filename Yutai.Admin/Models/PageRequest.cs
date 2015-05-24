using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yutai.Admin.Models
{
    public class PageRequest
    {
        public int Id { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}