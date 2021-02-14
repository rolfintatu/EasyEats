using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Intrastructure
{
    public partial class LittleChanges1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Month_Year",
                table: "Schedules",
                newName: "Year");

            migrationBuilder.RenameColumn(
                name: "Month_MountDays",
                table: "Schedules",
                newName: "NumberOfDays");

            migrationBuilder.RenameColumn(
                name: "Month_MonthNumber",
                table: "Schedules",
                newName: "Month");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Year",
                table: "Schedules",
                newName: "Month_Year");

            migrationBuilder.RenameColumn(
                name: "NumberOfDays",
                table: "Schedules",
                newName: "Month_MountDays");

            migrationBuilder.RenameColumn(
                name: "Month",
                table: "Schedules",
                newName: "Month_MonthNumber");
        }
    }
}
