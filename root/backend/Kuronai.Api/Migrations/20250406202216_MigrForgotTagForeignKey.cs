using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kuronai.Api.Migrations
{
    /// <inheritdoc />
    public partial class MigrForgotTagForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecipeTagId",
                table: "HouseholdTagOptions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "HouseholdTagOptions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HouseholdTagOptions_RecipeTagId",
                table: "HouseholdTagOptions",
                column: "RecipeTagId");

            migrationBuilder.AddForeignKey(
                name: "FK_HouseholdTagOptions_RecipeTags_RecipeTagId",
                table: "HouseholdTagOptions",
                column: "RecipeTagId",
                principalTable: "RecipeTags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HouseholdTagOptions_RecipeTags_RecipeTagId",
                table: "HouseholdTagOptions");

            migrationBuilder.DropIndex(
                name: "IX_HouseholdTagOptions_RecipeTagId",
                table: "HouseholdTagOptions");

            migrationBuilder.DropColumn(
                name: "RecipeTagId",
                table: "HouseholdTagOptions");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "HouseholdTagOptions");
        }
    }
}
