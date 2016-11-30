using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebView.Model;

namespace WebView.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebView.Model.Access", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("Token")
                        .IsRequired();

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Access");
                });

            modelBuilder.Entity("WebView.Model.Coment", b =>
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

                    b.ToTable("Coments");
                });

            modelBuilder.Entity("WebView.Model.Post", b =>
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

            modelBuilder.Entity("WebView.Model.Privilege", b =>
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

                    b.ToTable("Privileges");
                });

            modelBuilder.Entity("WebView.Model.Prize", b =>
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

            modelBuilder.Entity("WebView.Model.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Dislike");

                    b.Property<int>("Like");

                    b.HasKey("Id");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("WebView.Model.Security", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Security");
                });

            modelBuilder.Entity("WebView.Model.Tag", b =>
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

            modelBuilder.Entity("WebView.Model.TagCollection", b =>
                {
                    b.Property<int>("PostId");

                    b.Property<int>("TagId");

                    b.HasKey("PostId", "TagId");

                    b.HasIndex("PostId");

                    b.HasIndex("TagId");

                    b.ToTable("TagCollection");
                });

            modelBuilder.Entity("WebView.Model.User", b =>
                {
                    b.Property<string>("Id");

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

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebView.Model.Access", b =>
                {
                    b.HasOne("WebView.Model.User", "User")
                        .WithOne("Access")
                        .HasForeignKey("WebView.Model.Access", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebView.Model.Coment", b =>
                {
                    b.HasOne("WebView.Model.Coment", "Reply")
                        .WithMany()
                        .HasForeignKey("ComentId");

                    b.HasOne("WebView.Model.Post", "Post")
                        .WithMany("Coments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebView.Model.Rating", "Rating")
                        .WithMany()
                        .HasForeignKey("RatingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebView.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebView.Model.Post", b =>
                {
                    b.HasOne("WebView.Model.Rating", "Rating")
                        .WithMany()
                        .HasForeignKey("RatingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebView.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebView.Model.Privilege", b =>
                {
                    b.HasOne("WebView.Model.User", "User")
                        .WithMany("Privilege")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebView.Model.Prize", b =>
                {
                    b.HasOne("WebView.Model.User", "User")
                        .WithMany("Prize")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebView.Model.Security", b =>
                {
                    b.HasOne("WebView.Model.User", "User")
                        .WithOne("Security")
                        .HasForeignKey("WebView.Model.Security", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebView.Model.TagCollection", b =>
                {
                    b.HasOne("WebView.Model.Post", "Post")
                        .WithMany("Tags")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebView.Model.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
