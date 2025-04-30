using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kuronai.Api.Migrations
{
    /// <inheritdoc />
    public partial class RecipeCustomisation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HouseholdRecipeOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HouseholdId = table.Column<int>(type: "INTEGER", nullable: false),
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: false),
                    MinReoccurenceDelayWeeks = table.Column<int>(type: "INTEGER", nullable: false),
                    MinWeeklyOccurence = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxWeeklyOccurence = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseholdRecipeOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HouseholdRecipeOptions_Households_HouseholdId",
                        column: x => x.HouseholdId,
                        principalTable: "Households",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HouseholdRecipeOptions_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HouseholdTagOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HouseholdId = table.Column<int>(type: "INTEGER", nullable: false),
                    MinReoccurenceDelayWeeks = table.Column<int>(type: "INTEGER", nullable: false),
                    MinWeeklyOccurence = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxWeeklyOccurence = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseholdTagOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HouseholdTagOptions_Households_HouseholdId",
                        column: x => x.HouseholdId,
                        principalTable: "Households",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HouseholdRecipeOptions_HouseholdId",
                table: "HouseholdRecipeOptions",
                column: "HouseholdId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseholdRecipeOptions_RecipeId",
                table: "HouseholdRecipeOptions",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseholdTagOptions_HouseholdId",
                table: "HouseholdTagOptions",
                column: "HouseholdId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HouseholdRecipeOptions");

            migrationBuilder.DropTable(
                name: "HouseholdTagOptions");
        }
    }
}
