using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "category_model",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "VARCHAR(80)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category_model", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "book_model",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "VARCHAR(25)", nullable: false),
                    price = table.Column<int>(type: "INTEGER", nullable: false),
                    file_path = table.Column<string>(type: "VARCHAR(500)", nullable: false),
                    category_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book_model", x => x.id);
                    table.ForeignKey(
                        name: "FK_book_model_category_model_category_id",
                        column: x => x.category_id,
                        principalTable: "category_model",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "author_model",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    BookModelId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_author_model", x => x.id);
                    table.ForeignKey(
                        name: "FK_author_model_book_model_BookModelId",
                        column: x => x.BookModelId,
                        principalTable: "book_model",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "book_author",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "integer", nullable: false),
                    AuthorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book_author", x => new { x.BookId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_book_author_author_model_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "author_model",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_book_author_book_model_BookId",
                        column: x => x.BookId,
                        principalTable: "book_model",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_author_model_BookModelId",
                table: "author_model",
                column: "BookModelId");

            migrationBuilder.CreateIndex(
                name: "IX_book_author_AuthorId",
                table: "book_author",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_book_model_category_id",
                table: "book_model",
                column: "category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "book_author");

            migrationBuilder.DropTable(
                name: "author_model");

            migrationBuilder.DropTable(
                name: "book_model");

            migrationBuilder.DropTable(
                name: "category_model");
        }
    }
}
