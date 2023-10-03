using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lift.Buddy.Core.Migrations
{
    /// <inheritdoc />
    public partial class personalrecordnoexercise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_WorkoutDays_WorkoutDayId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalRecords_Exercises_ExerciseId",
                table: "PersonalRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityQuestions_Users_UserId",
                table: "SecurityQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutDays_WorkoutPlans_WorkoutPlanId",
                table: "WorkoutDays");

            migrationBuilder.DropIndex(
                name: "IX_PersonalRecords_ExerciseId",
                table: "PersonalRecords");

            migrationBuilder.DropColumn(
                name: "ExerciseId",
                table: "PersonalRecords");

            migrationBuilder.RenameColumn(
                name: "Reps",
                table: "PersonalRecords",
                newName: "Repetitions");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "WorkoutPlans",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "WorkoutPlanId",
                table: "WorkoutDays",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "SecurityQuestions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Question",
                table: "SecurityQuestions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Answer",
                table: "SecurityQuestions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "ExerciseName",
                table: "PersonalRecords",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "ExerciseType",
                table: "PersonalRecords",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "WorkoutDayId",
                table: "Exercises",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Exercises",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_WorkoutDays_WorkoutDayId",
                table: "Exercises",
                column: "WorkoutDayId",
                principalTable: "WorkoutDays",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityQuestions_Users_UserId",
                table: "SecurityQuestions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutDays_WorkoutPlans_WorkoutPlanId",
                table: "WorkoutDays",
                column: "WorkoutPlanId",
                principalTable: "WorkoutPlans",
                principalColumn: "WorkoutPlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_WorkoutDays_WorkoutDayId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityQuestions_Users_UserId",
                table: "SecurityQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutDays_WorkoutPlans_WorkoutPlanId",
                table: "WorkoutDays");

            migrationBuilder.DropColumn(
                name: "ExerciseType",
                table: "PersonalRecords");

            migrationBuilder.RenameColumn(
                name: "Repetitions",
                table: "PersonalRecords",
                newName: "Reps");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "WorkoutPlans",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "WorkoutPlanId",
                table: "WorkoutDays",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "SecurityQuestions",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Question",
                table: "SecurityQuestions",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Answer",
                table: "SecurityQuestions",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExerciseName",
                table: "PersonalRecords",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ExerciseId",
                table: "PersonalRecords",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "WorkoutDayId",
                table: "Exercises",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Exercises",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalRecords_ExerciseId",
                table: "PersonalRecords",
                column: "ExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_WorkoutDays_WorkoutDayId",
                table: "Exercises",
                column: "WorkoutDayId",
                principalTable: "WorkoutDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalRecords_Exercises_ExerciseId",
                table: "PersonalRecords",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "ExerciseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityQuestions_Users_UserId",
                table: "SecurityQuestions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutDays_WorkoutPlans_WorkoutPlanId",
                table: "WorkoutDays",
                column: "WorkoutPlanId",
                principalTable: "WorkoutPlans",
                principalColumn: "WorkoutPlanId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
