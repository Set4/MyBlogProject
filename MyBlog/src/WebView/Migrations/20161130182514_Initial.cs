using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebView.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FullTag = table.Column<string>(maxLength: 100, nullable: true),
                    Type = table.Column<int>(nullable: false),
                    ViewTag = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
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
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Access",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Token = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Access", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Access_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                        name: "FK_Posts_Users_Author",
                        column: x => x.Author,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Privileges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Author = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Privileges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Privileges_Users_Author",
                        column: x => x.Author,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prize",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImagePath = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Author = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prize", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prize_Users_Author",
                        column: x => x.Author,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Security",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Security", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Security_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                    table.PrimaryKey("PK_Coments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coments_Coments_ComentId",
                        column: x => x.ComentId,
                        principalTable: "Coments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Coments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Coments_Rating_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Rating",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Coments_Users_Author",
                        column: x => x.Author,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
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
                name: "IX_Coments_ComentId",
                table: "Coments",
                column: "ComentId");

            migrationBuilder.CreateIndex(
                name: "IX_Coments_PostId",
                table: "Coments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Coments_RatingId",
                table: "Coments",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_Coments_Author",
                table: "Coments",
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
                name: "IX_Privileges_Author",
                table: "Privileges",
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
                name: "IX_TagCollection_PostId",
                table: "TagCollection",
                column: "PostId");

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
                name: "Coments");

            migrationBuilder.DropTable(
                name: "Privileges");

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
                name: "Users");
        }
    }
}
