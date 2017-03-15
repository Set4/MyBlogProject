using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebBlog.DataAccess.Model
{
    public class Coment:ModelBase
    {
        public Guid IdPost { get; set; }

        public Guid IdUser { get; set; }

        public Guid IdRating { get; set; }

        public Guid? IdComent { get; set; }


        [Required, MaxLength(500)]
        public string Text { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        public DateTime DateChange { get; set; }

        public StateComent State { get; set; }
    }


    public enum StateComent
    {
        Public,
        Removed, 
        Blocked,
        Changed,
    }

}
