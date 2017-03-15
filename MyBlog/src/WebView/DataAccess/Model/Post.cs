using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebBlog.DataAccess.Model
{


    public class Post : ModelBase
    {
        [Required, MaxLength(300)]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int Views { get; set; }

        [Required]
        public StatePost State { get; set; }

        public Rating Rating { get; set; }

        public List<Tag> Tags { get; set; }

        [Required]
        public User Author { get; set; }

        public Category Category { get; set; }

        public List<Coment> Coments { get; set; }
    }


    public enum StatePost
    {
        Public = 0,
        Removed = 1,
        Hidden = 2,
        Locked = 3
    }

    public class Category : ModelBase
    {
        [Required]
        public string Name { get; set; }

        public string ImagePath { get; set; }
    }

}
