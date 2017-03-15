using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebBlog.DataAccess.Model
{
    public class TagCollection:ModelBase
    {
        public Guid IdPost { get; set; }
        public Guid IdTag { get; set; }
    }

    public class Tag : ModelBase
    {
        [MaxLength(100)]
        public string FullTag { get; set; }
        [MaxLength(10)]
        public string ViewTag { get; set; }

        public TagTypeEnum Type { get; set; }

        public TagColor Color { get; set; }
    }

    public enum TagTypeEnum
    {
        Public = 0,
        Removed = 1,
        Hidden = 2,
        Locked = 3
    }

    public enum TagColor
    {
        Red = 0,
        Removed = 1,
        Hidden = 2,
        Locked = 3
    }
}
