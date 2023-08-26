using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lift.Buddy.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddedWorkoutUserRelationAndName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "WorkoutSchedules",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "WorkoutAssignments",
                columns: table => new
                {
                    WorkoutUser = table.Column<string>(type: "TEXT", nullable: false),
                    WorkoutId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutAssignments", x => x.WorkoutUser);
                    table.ForeignKey(
                        name: "FK_WorkoutAssignments_Users_WorkoutUser",
                        column: x => x.WorkoutUser,
                        principalTable: "Users",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutAssignments_WorkoutSchedules_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "WorkoutSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutAssignments_WorkoutId",
                table: "WorkoutAssignments",
                column: "WorkoutId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkoutAssignments");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "WorkoutSchedules");
        }
    }
}
