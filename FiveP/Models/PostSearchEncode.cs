using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiveP.Models
{
    public class PostSearchEncode
    {
        public string post_id { get; set; }
        public string post_title { get; set; }
        public Nullable<int> post_popular { get; set; }
    }
}