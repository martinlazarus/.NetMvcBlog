using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog40.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}