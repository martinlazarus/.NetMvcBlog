using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog40.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Blog40.ViewModels
{
    public class PostEditViewModel
    {
        public Post Post { get; set; }
        public List<Category> Categories { get; set; }
        public List<Author> Authors { get; set; }
    }
}