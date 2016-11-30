using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary1
{
    public class DataBaseContext:DbContext
    {
        //public DataBaseContext(DbContextOptions<DataBaseContext> options):base(options)
        //{

        //}
        public DbSet<Student> Students { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFCoreStudentDb;Trusted_Connection=True;");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //}
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
