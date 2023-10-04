using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lift.Buddy.Core.Migrations
{
    /// <inheritdoc />
    public partial class ondeletecascadeworkout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_WorkoutDays_WorkoutDayId",
                table: "Exercises");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_WorkoutDays_WorkoutDayId",
                table: "Exercises",
                column: "WorkoutDayId",
                principalTable: "WorkoutDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_WorkoutDays_WorkoutDayId",
                table: "Exercises");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_WorkoutDays_WorkoutDayId",
                table: "Exercises",
                column: "WorkoutDayId",
                principalTable: "WorkoutDays",
                principalColumn: "Id");
        }
    }
}
