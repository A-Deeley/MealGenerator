using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kuronai.Api.Migrations
{
    /// <inheritdoc />
    public partial class SupportForRecipeTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecipeTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Colour = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipeRecipeTag",
                columns: table => new
                {
                    RecipeTagsId = table.Column<int>(type: "INTEGER", nullable: false),
                    RecipesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeRecipeTag", x => new { x.RecipeTagsId, x.RecipesId });
                    table.ForeignKey(
                        name: "FK_RecipeRecipeTag_RecipeTags_RecipeTagsId",
                        column: x => x.RecipeTagsId,
                        principalTable: "RecipeTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeRecipeTag_Recipes_RecipesId",
                        column: x => x.RecipesId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeRecipeTag_RecipesId",
                table: "RecipeRecipeTag",
                column: "RecipesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeRecipeTag");

            migrationBuilder.DropTable(
                name: "RecipeTags");
        }
    }
}
