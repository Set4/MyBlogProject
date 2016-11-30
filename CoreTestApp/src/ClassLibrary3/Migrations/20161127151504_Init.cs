using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClassLibrary3.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Dislike = table.Column<int>(nullable: false),
                    Like = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullTag = table.Column<string>(maxLength: 100, nullable: true),
                    Type = table.Column<int>(nullable: false),
                    ViewTag = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 30, nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(maxLength: 30, nullable: true),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    StateElement = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Access",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Token = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Access", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Access_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(nullable: false),
                    DateChange = table.Column<DateTime>(nullable: false),
                    RatingId = table.Column<int>(nullable: false),
                    StateElement = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    Title = table.Column<string>(maxLength: 300, nullable: false),
                    Author = table.Column<string>(nullable: false),
                    Views = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Rating_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Rating",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_User_Author",
                        column: x => x.Author,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Privilege",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Author = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Privilege", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Privilege_User_Author",
                        column: x => x.Author,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prize",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ImagePath = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Author = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prize", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prize_User_Author",
                        column: x => x.Author,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Security",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Security", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Security_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Coment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ComentId = table.Column<int>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateChange = table.Column<DateTime>(nullable: false),
                    PostId = table.Column<int>(nullable: false),
                    RatingId = table.Column<int>(nullable: false),
                    StateElement = table.Column<int>(nullable: false),
                    Text = table.Column<string>(maxLength: 500, nullable: false),
                    Author = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coment_Coment_ComentId",
                        column: x => x.ComentId,
                        principalTable: "Coment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Coment_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Coment_Rating_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Rating",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Coment_User_Author",
                        column: x => x.Author,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagCollection",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagCollection", x => new { x.PostId, x.TagId });
                    table.ForeignKey(
                        name: "FK_TagCollection_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagCollection_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Access_UserId",
                table: "Access",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coment_ComentId",
                table: "Coment",
                column: "ComentId");

            migrationBuilder.CreateIndex(
                name: "IX_Coment_PostId",
                table: "Coment",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Coment_RatingId",
                table: "Coment",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_Coment_Author",
                table: "Coment",
                column: "Author");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_RatingId",
                table: "Posts",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Author",
                table: "Posts",
                column: "Author");

            migrationBuilder.CreateIndex(
                name: "IX_Privilege_Author",
                table: "Privilege",
                column: "Author");

            migrationBuilder.CreateIndex(
                name: "IX_Prize_Author",
                table: "Prize",
                column: "Author");

            migrationBuilder.CreateIndex(
                name: "IX_Security_UserId",
                table: "Security",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TagCollection_TagId",
                table: "TagCollection",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Access");

            migrationBuilder.DropTable(
                name: "Coment");

            migrationBuilder.DropTable(
                name: "Privilege");

            migrationBuilder.DropTable(
                name: "Prize");

            migrationBuilder.DropTable(
                name: "Security");

            migrationBuilder.DropTable(
                name: "TagCollection");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
