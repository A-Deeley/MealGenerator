using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kuronai.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Households",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    OwnerId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Households", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Households_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HouseholdRecipe",
                columns: table => new
                {
                    HouseholdsId = table.Column<int>(type: "INTEGER", nullable: false),
                    RecipesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseholdRecipe", x => new { x.HouseholdsId, x.RecipesId });
                    table.ForeignKey(
                        name: "FK_HouseholdRecipe_Households_HouseholdsId",
                        column: x => x.HouseholdsId,
                        principalTable: "Households",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HouseholdRecipe_Recipes_RecipesId",
                        column: x => x.RecipesId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HouseholdUser",
                columns: table => new
                {
                    MemberOfId = table.Column<int>(type: "INTEGER", nullable: false),
                    MembersId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseholdUser", x => new { x.MemberOfId, x.MembersId });
                    table.ForeignKey(
                        name: "FK_HouseholdUser_Households_MemberOfId",
                        column: x => x.MemberOfId,
                        principalTable: "Households",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HouseholdUser_Users_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HouseholdRecipe_RecipesId",
                table: "HouseholdRecipe",
                column: "RecipesId");

            migrationBuilder.CreateIndex(
                name: "IX_Households_OwnerId",
                table: "Households",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseholdUser_MembersId",
                table: "HouseholdUser",
                column: "MembersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HouseholdRecipe");

            migrationBuilder.DropTable(
                name: "HouseholdUser");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Households");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
