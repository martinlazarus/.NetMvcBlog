using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Blog40.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public Author Author { get; set; }
        public Category Category { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool isDeleted { get; set; }

        public Post()
        {
            this.Category = new Category();
            this.Author = new Author();
        }
    
    }
}