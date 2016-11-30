using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ClassLibrary3;

namespace ClassLibrary3.Migrations
{
    [DbContext(typeof(BloggingContext))]
    partial class BloggingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("ClassLibrary3.Access", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Token")
                        .IsRequired();

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Access");
                });

            modelBuilder.Entity("ClassLibrary3.Coment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ComentId");

                    b.Property<DateTime>("DateAdded");

                    b.Property<DateTime>("DateChange");

                    b.Property<int>("PostId");

                    b.Property<int>("RatingId");

                    b.Property<int>("StateElement");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 500);

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnName("Author");

                    b.HasKey("Id");

                    b.HasIndex("ComentId");

                    b.HasIndex("PostId");

                    b.HasIndex("RatingId");

                    b.HasIndex("UserId");

                    b.ToTable("Coment");
                });

            modelBuilder.Entity("ClassLibrary3.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("DateChange");

                    b.Property<int>("RatingId");

                    b.Property<int>("StateElement");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 300);

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnName("Author");

                    b.Property<int>("Views");

                    b.HasKey("Id");

                    b.HasIndex("RatingId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("ClassLibrary3.Privilege", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnName("Author");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Privilege");
                });

            modelBuilder.Entity("ClassLibrary3.Prize", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImagePath");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnName("Author");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Prize");
                });

            modelBuilder.Entity("ClassLibrary3.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Dislike");

                    b.Property<int>("Like");

                    b.HasKey("Id");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("ClassLibrary3.Security", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Security");
                });

            modelBuilder.Entity("ClassLibrary3.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FullTag")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int>("Type");

                    b.Property<string>("ViewTag")
                        .HasAnnotation("MaxLength", 30);

                    b.HasKey("Id");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("ClassLibrary3.TagCollection", b =>
                {
                    b.Property<int>("PostId");

                    b.Property<int>("TagId");

                    b.HasKey("PostId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("TagCollection");
                });

            modelBuilder.Entity("ClassLibrary3.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("ImagePath");

                    b.Property<string>("LastName")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.Property<int>("StateElement");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ClassLibrary3.Access", b =>
                {
                    b.HasOne("ClassLibrary3.User", "User")
                        .WithOne("Access")
                        .HasForeignKey("ClassLibrary3.Access", "UserId");
                });

            modelBuilder.Entity("ClassLibrary3.Coment", b =>
                {
                    b.HasOne("ClassLibrary3.Coment", "Reply")
                        .WithMany()
                        .HasForeignKey("ComentId");

                    b.HasOne("ClassLibrary3.Post", "Post")
                        .WithMany("Coments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ClassLibrary3.Rating", "Rating")
                        .WithMany()
                        .HasForeignKey("RatingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ClassLibrary3.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ClassLibrary3.Post", b =>
                {
                    b.HasOne("ClassLibrary3.Rating", "Rating")
                        .WithMany()
                        .HasForeignKey("RatingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ClassLibrary3.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ClassLibrary3.Privilege", b =>
                {
                    b.HasOne("ClassLibrary3.User", "User")
                        .WithMany("Privilege")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ClassLibrary3.Prize", b =>
                {
                    b.HasOne("ClassLibrary3.User", "User")
                        .WithMany("Prize")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ClassLibrary3.Security", b =>
                {
                    b.HasOne("ClassLibrary3.User", "User")
                        .WithOne("Security")
                        .HasForeignKey("ClassLibrary3.Security", "UserId");
                });

            modelBuilder.Entity("ClassLibrary3.TagCollection", b =>
                {
                    b.HasOne("ClassLibrary3.Post", "Post")
                        .WithMany("Tags")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ClassLibrary3.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
