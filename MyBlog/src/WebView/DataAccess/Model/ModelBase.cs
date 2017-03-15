using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBlog.DataAccess.Model
{
    public abstract class ModelBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
