using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Intrastructure
{
    public partial class LittleChanges2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Schedules_ScheduleId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ScheduleId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "NumberOfDays",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Schedules");

            migrationBuilder.AddColumn<Guid>(
                name: "MonthScheduleId",
                table: "Schedules",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Month",
                columns: table => new
                {
                    ScheduleId = table.Column<Guid>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    NumberOfDays = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Month", x => x.ScheduleId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_MonthScheduleId",
                table: "Schedules",
                column: "MonthScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Month_MonthScheduleId",
                table: "Schedules",
                column: "MonthScheduleId",
                principalTable: "Month",
                principalColumn: "ScheduleId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Month_MonthScheduleId",
                table: "Schedules");

            migrationBuilder.DropTable(
                name: "Month");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_MonthScheduleId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "MonthScheduleId",
                table: "Schedules");

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "Schedules",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfDays",
                table: "Schedules",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Schedules",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ScheduleId",
                table: "Reservations",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Schedules_ScheduleId",
                table: "Reservations",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
