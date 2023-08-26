using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lift.Buddy.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddedPRTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserPRs",
                columns: table => new
                {
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    PersonalRecords = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPRs", x => x.Username);
                    table.ForeignKey(
                        name: "FK_UserPRs_Users_Username",
                        column: x => x.Username,
                        principalTable: "Users",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPRs");
        }
    }
}
