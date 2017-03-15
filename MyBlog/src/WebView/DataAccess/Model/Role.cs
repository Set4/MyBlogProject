using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebBlog.DataAccess.Model
{
    public class Role: ModelBase
    {
        [Required, MaxLength(20)]
        public string Name { get; set; }

        public List<Permission> Permission { get; set; }
    }

    public class RoleCollection : ModelBase
    {
        public Guid IdUser { get; set; }
        public Guid IdRole { get; set; }
    }

    public enum Permission
    {
        Read,
        Write,
        Removed,
        Blocked,
    }
}
