using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lift.Buddy.Core.Migrations
{
    /// <inheritdoc />
    public partial class userpropertyrename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWorkoutPlan_WorkoutPlans_WorkoutPlansWorkoutPlanId",
                table: "UserWorkoutPlan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserWorkoutPlan",
                table: "UserWorkoutPlan");

            migrationBuilder.DropIndex(
                name: "IX_UserWorkoutPlan_WorkoutPlansWorkoutPlanId",
                table: "UserWorkoutPlan");

            migrationBuilder.RenameColumn(
                name: "WorkoutPlansWorkoutPlanId",
                table: "UserWorkoutPlan",
                newName: "AssignedPlansWorkoutPlanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserWorkoutPlan",
                table: "UserWorkoutPlan",
                columns: new[] { "AssignedPlansWorkoutPlanId", "UsersUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkoutPlan_UsersUserId",
                table: "UserWorkoutPlan",
                column: "UsersUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWorkoutPlan_WorkoutPlans_AssignedPlansWorkoutPlanId",
                table: "UserWorkoutPlan",
                column: "AssignedPlansWorkoutPlanId",
                principalTable: "WorkoutPlans",
                principalColumn: "WorkoutPlanId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWorkoutPlan_WorkoutPlans_AssignedPlansWorkoutPlanId",
                table: "UserWorkoutPlan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserWorkoutPlan",
                table: "UserWorkoutPlan");

            migrationBuilder.DropIndex(
                name: "IX_UserWorkoutPlan_UsersUserId",
                table: "UserWorkoutPlan");

            migrationBuilder.RenameColumn(
                name: "AssignedPlansWorkoutPlanId",
                table: "UserWorkoutPlan",
                newName: "WorkoutPlansWorkoutPlanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserWorkoutPlan",
                table: "UserWorkoutPlan",
                columns: new[] { "UsersUserId", "WorkoutPlansWorkoutPlanId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkoutPlan_WorkoutPlansWorkoutPlanId",
                table: "UserWorkoutPlan",
                column: "WorkoutPlansWorkoutPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWorkoutPlan_WorkoutPlans_WorkoutPlansWorkoutPlanId",
                table: "UserWorkoutPlan",
                column: "WorkoutPlansWorkoutPlanId",
                principalTable: "WorkoutPlans",
                principalColumn: "WorkoutPlanId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
