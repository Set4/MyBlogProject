using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebBlog.DataAccess.Model
{

    public class Prize:ModelBase
    {
        [Required, MaxLength(20)]
        public string Name { get; set; }
        public string ImagePath { get; set; }
    }

    public class PrizeCollection : ModelBase
    {
        public Guid IdUser { get; set; }
        public Guid IdPrize { get; set; }
    }

}
