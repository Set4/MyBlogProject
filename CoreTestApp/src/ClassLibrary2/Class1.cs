using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public class DataBaseContext : DbContext
    {
      
        public DataBaseContext( DbContextOptions<DataBaseContext> options):base(options)
        {

        }
        public DbSet<User> Users { get; set; }


    
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
