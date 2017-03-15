using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBlog.DataAccess.Model
{

    public class Rating:ModelBase
    {
        public int Like { get; set; }
        public int Dislike { get; set; }
    }

}
