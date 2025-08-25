using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class StoreTimeSpanAsTicksForShiftTimes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "StartTime",
                table: "Shifts",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<long>(
                name: "EndTime",
                table: "Shifts",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_Date_StartTime",
                table: "Shifts",
                columns: new[] { "Date", "StartTime" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Shifts_Date_StartTime",
                table: "Shifts");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartTime",
                table: "Shifts",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndTime",
                table: "Shifts",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER");
        }
    }
}
