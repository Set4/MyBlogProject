using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.DataAccess.Model;

namespace WebBlog.DataAccess.ViewModel
{
    public class PostView : ModelBase
    {


        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public DateTime DateChange { get; set; }

        public int Views { get; set; }

        public StatePost State { get; set; }



        ////////// Category
        public string NameCategory { get; set; }
        public string ImagePathCategory { get; set; }
        ////////// 


        ////////// Rating
        public int Like { get; set; }
        public int Dislike { get; set; }
        ////////// 


        ////////// Author
        public string UserName { get; set; }
        public string ImagePath { get; set; }
        ////////// 


        ////////// TagCollection
        public List<Tag> Tags { get; set; }
        ////////// 
    }


}
