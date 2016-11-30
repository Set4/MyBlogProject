using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibrary3
{
    public class BloggingContext : DbContext
    {
      
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./Blogging.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Tag>();
            //modelBuilder.Entity<TagCollection>();
            //modelBuilder.Entity<Security>();
            // modelBuilder.Entity<State>();

       
            modelBuilder.Entity<TagCollection>()
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

    }


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
        public string Id { get; set; }


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


    }






    /*
        public class Blog
    {
        [Key]
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; set; }
    }

    public class Post
    {
         [Key]
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
    */
}
