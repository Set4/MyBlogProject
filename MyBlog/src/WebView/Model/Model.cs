﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace WebView.Model
{



    public enum StateEnum
    {
        Public = 0,
        Removed = 1,
        Hidden = 2,
        Locked = 3
    }




    public class Post
    {
        public int Id { get; set; }
        [Required, MaxLength(300)]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime Date { get; set; }
        public DateTime DateChange { get; set; }


        public int Views { get; set; }

        [Required]
        public StateEnum StateElement { get; set; }

        [ForeignKey("Ratings")]
        public int RatingId { get; set; }
        public Rating Rating { get; set; }

        public ICollection<TagCollection> Tags { get; set; }

        [Column("Author"), Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }


        public ICollection<Coment> Coments { get; set; }



        public Post()
        {
            Tags = new List<TagCollection>();
        }

        public Post(string title, string text, User author, List<TagCollection> tags, StateEnum state)
        {
            Title = title;
            Text = text;
            User = author;
            Date = DateTime.Now;
            Tags = tags;
            StateElement = state;

            Coments = new List<Coment>();
        }
    }

    public class TagCollection
    {
        public int PostId { get; set; }
        public Post Post { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }

    public class Tag
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string FullTag { get; set; }
        [MaxLength(30)]
        public string ViewTag { get; set; }

        public TypeTagEnum Type { get; set; }
    }

    public enum TypeTagEnum
    {
        Public = 0,
        Removed = 1,
        Hidden = 2,
        Locked = 3
    }


    public class Rating
    {
        public int Id { get; set; }

        public int Like { get; set; }
        public int Dislike { get; set; }
    }








    public class User
    {
        string id;
        public string Id
        {
            get
            {
               return id;
            }
            set
            {
                if (id==null)
                    id = SequentialGuidGenerator.CreateGuid().ToByteArray().ToString();
                    //  Guid.NewGuid().ToString();
            }
        }


        [Required, MaxLength(20)]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [MaxLength(30)]
        public string FirstName { get; set; }
        [MaxLength(30)]
        public string LastName { get; set; }

        public DateTime Date { get; set; }

        public StateEnum StateElement { get; set; }


        public ICollection<Privilege> Privilege { get; set; }

        public ICollection<Prize> Prize { get; set; }

        public string ImagePath { get; set; }








        [Required]
        public Security Security { get; set; }

        [Required]
        public Access Access { get; set; }



        public User()
        { }

        public User(string name, string email)
        {
            this.Name = name;
            this.Email = email;
            Date = DateTime.Now;
        }

    }

    public class Prize
    {
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string Name { get; set; }

        public string ImagePath { get; set; }

        [Column("Author"), Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }


    public class Privilege
    {
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string Name { get; set; }

        [Column("Author"), Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }






    public class Security
    {
        public string Id { get; set; }
        [Required]
        public string Password { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public Security()
        { 
        }

        public Security(string IdUser, string Password)
        {
            this.Id = IdUser;
            this.Password = Password;
        }
    }

    public class Access
    {
        public string Id { get; set; }
        [Required]
        public string Token { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }




    public class Coment
    {
        public int Id { get; set; }


        [Required]
        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }

        [Column("Author"), Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }


        public int RatingId { get; set; }
        [ForeignKey("RatingId")]
        public Rating Rating { get; set; }


        public int? ComentId { get; set; }
        [ForeignKey("ComentId")]
        public virtual Coment Reply { get; set; }

        [Required, MaxLength(500)]
        public string Text { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        public DateTime DateChange { get; set; }

        [Required]
        public StateEnum StateElement { get; set; }

        public Coment()
        {
        }

        public Coment(int PostId, string UserId, string Text, int? ComentId=null)
        {
            this.PostId = PostId;
            this.UserId = UserId;
            this.Text = Text;

            if (ComentId!=null)
            this.ComentId = ComentId;

            this.DateAdded = DateTime.Now;
        }
    }

    
    public class DataBaseContext : DbContext
    {

        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
        }


        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Privilege> Privileges { get; set; }
        public DbSet<Coment> Coments { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //   //optionsBuilder.UseSqlServer(@"Server=./SQLEXPRESS;Database=blogappdb;Trusted_Connection=True;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>();
            modelBuilder.Entity<TagCollection>();
            modelBuilder.Entity<Security>();
            modelBuilder.Entity<Rating>();



            modelBuilder
            .Entity<TagCollection>()
            .HasKey(t => new { t.PostId, t.TagId });


            modelBuilder
           .Entity<User>()
           .HasOne(u => u.Security)
           .WithOne(p => p.User)
           .HasForeignKey<Security>(p => p.UserId);

            modelBuilder
           .Entity<User>()
           .HasOne(u => u.Access)
           .WithOne(p => p.User)
           .HasForeignKey<Access>(p => p.UserId);
        }

      

      

        //public DataBaseContext() 
        //{
        //    var builder = new ConfigurationBuilder();
        //    // установка пути к текущему каталогу
        //    builder.SetBasePath(Directory.GetCurrentDirectory());
        //    // получаем конфигурацию из файла appsettings.json
        //    builder.AddJsonFile("appsettings.json");
        //    // создаем конфигурацию
        //    var config = builder.Build();
        //    // получаем строку подключения
        //    string connectionString = config.GetConnectionString("DataBaseConnection");

        //    var optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>();
        //    var options = optionsBuilder
        //        .UseSqlServer(connectionString)
        //        .Options;

        //}
    }
   


    public class NativeMethods
    {
        [DllImport("rpcrt4.dll", SetLastError = true)]
        public static extern int UuidCreateSequential(out Guid guid);
    }

    public static class SequentialGuidGenerator
    {
        public static Guid CreateGuid()
        {
            const int RPC_S_OK = 0;

            Guid guid;
            int result = NativeMethods.UuidCreateSequential(out guid);
            if (result == RPC_S_OK)
                return guid;
            else
                return Guid.NewGuid();
        }
    }


    public static class CodeGenerator
    {
        private static Random random; 
        public static string RandomString(int sizeCode)
        {
            random = new Random((int)DateTime.Now.Ticks);//thanks to McAden
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < sizeCode; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }

    }
}