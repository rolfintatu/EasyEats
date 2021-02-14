using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Intrastructure
{
    public partial class LittleChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_schedules_scheduleId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_schedules",
                table: "schedules");

            migrationBuilder.RenameTable(
                name: "schedules",
                newName: "Schedules");

            migrationBuilder.RenameColumn(
                name: "scheduleId",
                table: "Reservations",
                newName: "ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_scheduleId",
                table: "Reservations",
                newName: "IX_Reservations_ScheduleId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ScheduleId",
                table: "Reservations",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Schedules_ScheduleId",
                table: "Reservations",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Schedules_ScheduleId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules");

            migrationBuilder.RenameTable(
                name: "Schedules",
                newName: "schedules");

            migrationBuilder.RenameColumn(
                name: "ScheduleId",
                table: "Reservations",
                newName: "scheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_ScheduleId",
                table: "Reservations",
                newName: "IX_Reservations_scheduleId");

            migrationBuilder.AlterColumn<Guid>(
                name: "scheduleId",
                table: "Reservations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddPrimaryKey(
                name: "PK_schedules",
                table: "schedules",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_schedules_scheduleId",
                table: "Reservations",
                column: "scheduleId",
                principalTable: "schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
