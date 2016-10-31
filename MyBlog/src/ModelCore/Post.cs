﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.IO;
using System.Runtime.InteropServices;



namespace Core
{

    public enum StateEnum
    {
        Public = 0,
        Removed = 1,
        Hidden = 2,
        Locked = 3
    }

    public class State
    {
        public int Id { get; set; }

       // [Range(1, 3)]
        public StateEnum StateElement { get; set; }
    }


    public class Post
    {
        public int Id { get; set; }


        [Required, MaxLength(300)]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }

        [Column("Author"), Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int Views { get; set; }


        [ForeignKey("Ratings")]
        public int RatingId { get; set; }
        public Rating Rating { get; set; }

        [ForeignKey("State")]
        public int StateId { get; set; }
        public State State { get; set; }


        public ICollection<Coment> Coments { get; set; }


        public ICollection<TagCollection> Tags { get; set; }

        public Post()
        {
            Tags = new List<TagCollection>();
        }

        public Post(string title, string text, User author, DateTime dateCreate, List<TagCollection> tags, State state)
        {
            Title = title;
            Text = text;
            User = author;
            Date = dateCreate;
            Tags = tags;
            State = state;

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

        public string Type { get; set; }
    }

    public class User
    {
        byte[] id;
        public byte[] Id
        {
            get
            {
               return id;
            }
            set
            {
                if (id==null)
                    id = SequentialGuidGenerator.CreateGuid().ToByteArray();
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

        [ForeignKey("State")]
        public int StateId { get; set; }
        public State State { get; set; }

        [Required]
        public Security Security { get; set; }

        public ICollection<Privilege> Privilege { get; set; }
    }

    public class Security
    {
        [ForeignKey("Users")]
        public string Id { get; set; }
        [Required]
        public string Password { get; set; }
        public string Token { get; set; }

    }

    public class Privilege
    {
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string Name { get; set; }

        [Column("Author"), Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }



    
    public class Coment
    {
        public int Id { get; set; }

        [Required]
        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        [Column("Author"), Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Ratings")]
        public int RatingId { get; set; }
        public Rating Rating { get; set; }

        [ForeignKey("Coment")]
        public int? ComentId { get; set; }
        public virtual Coment Reply { get; set; }

        [Required, MaxLength(500)] 
        public string Text { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        public DateTime DateChanget { get; set; }

        [ForeignKey("State")]
        public int StateId { get; set; }
        public State State { get; set; }
    }

   
    public class Rating
    {
        public int Id { get; set; }

        public int Like { get; set; }
        public int Dislike { get; set; }
    }



  


    public class DataBaseContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Privilege> Privileges { get; set; }
        public DbSet<Coment> Coments { get; set; }

      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>();
            modelBuilder.Entity<TagCollection>();
            modelBuilder.Entity<Security>();
            modelBuilder.Entity<Rating>();

          
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=./SQLEXPRESS;Database=blogappdb;Trusted_Connection=True;");
        }

        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
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

}
