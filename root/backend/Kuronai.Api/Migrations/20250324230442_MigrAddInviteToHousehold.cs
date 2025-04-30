using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kuronai.Api.Migrations
{
    /// <inheritdoc />
    public partial class MigrAddInviteToHousehold : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HouseholdUserInvites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HouseholdId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseholdUserInvites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HouseholdUserInvites_Households_HouseholdId",
                        column: x => x.HouseholdId,
                        principalTable: "Households",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HouseholdUserInvites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HouseholdUserInvites_HouseholdId",
                table: "HouseholdUserInvites",
                column: "HouseholdId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseholdUserInvites_UserId",
                table: "HouseholdUserInvites",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HouseholdUserInvites");
        }
    }
}
