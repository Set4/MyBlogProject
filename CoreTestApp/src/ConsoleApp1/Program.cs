using ClassLibrary1;
using ClassLibrary2;
using ClassLibrary3;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Program
    {

        //для classlibrary2
        ///или испоользовать
        /// 
        /// using (var context = serviceProvider.GetService<BloggingContext>())
        /// {
        ///        // do stuff
        /// }
        ///
        ///  var options = serviceProvider.GetService<DbContextOptions<BloggingContext>>();
        /// 
        /// 
        public class DbContextFactory : IDbContextFactory<ClassLibrary2.DataBaseContext>
        {
            public ClassLibrary2.DataBaseContext Create()
            {
                var builder = new DbContextOptionsBuilder<ClassLibrary2.DataBaseContext>();
                builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EFCoreUserDb;Trusted_Connection=True;MultipleActiveResultSets=true");
                return new ClassLibrary2.DataBaseContext(builder.Options);
            }

            public ClassLibrary2.DataBaseContext Create(DbContextFactoryOptions options)
            {
                var builder = new DbContextOptionsBuilder<ClassLibrary2.DataBaseContext>();
                builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EFCoreUserDb;Trusted_Connection=True;MultipleActiveResultSets=true");
                return new ClassLibrary2.DataBaseContext(builder.Options);
            }
        }


        public static void Main(string[] args)
        {

         
            using (var connet = new ClassLibrary1.DataBaseContext())
            {
               Console.WriteLine("Rjkbxtcndj 'ktvtynjd={0}",connet.Students.Count().ToString());
            }



            var options = new DbContextOptionsBuilder<ClassLibrary2.DataBaseContext>();
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFCoreUserDb;Trusted_Connection=True;");


          
            using (var connet = new ClassLibrary2.DataBaseContext(options.Options))
            {
                Console.WriteLine("Rjkbxtcndj 'ktvtynjd={0}", connet.Users.Count().ToString());
            }


            using (var db = new BloggingContext())
            {
                try
                {
                    db.Database.Migrate();

                    db.Posts.Add(new Post() { Text = "Text", Title = "Title", StateElement = StateEnum.Public });
                    var count = db.SaveChanges();
                 
                    Console.WriteLine("Rjkbxtcndj 'ktvtynjd={0}", db.Posts.Count().ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error:{0}", ex.Message);
                }
            }
       

            Console.ReadLine();

        }
    }
}
