using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class ModifyDrinkTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_OrderDetails_OrderDetailsId",
                table: "Drinks");

            migrationBuilder.AlterColumn<int>(
                name: "OrderDetailsId",
                table: "Drinks",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Drinks_OrderDetails_OrderDetailsId",
                table: "Drinks",
                column: "OrderDetailsId",
                principalTable: "OrderDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_OrderDetails_OrderDetailsId",
                table: "Drinks");

            migrationBuilder.AlterColumn<int>(
                name: "OrderDetailsId",
                table: "Drinks",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Drinks_OrderDetails_OrderDetailsId",
                table: "Drinks",
                column: "OrderDetailsId",
                principalTable: "OrderDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
