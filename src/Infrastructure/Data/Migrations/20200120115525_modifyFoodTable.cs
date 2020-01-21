using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class modifyFoodTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_OrderDetails_OrderDetailsId",
                table: "Foods");

            migrationBuilder.AlterColumn<int>(
                name: "OrderDetailsId",
                table: "Foods",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_OrderDetails_OrderDetailsId",
                table: "Foods",
                column: "OrderDetailsId",
                principalTable: "OrderDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_OrderDetails_OrderDetailsId",
                table: "Foods");

            migrationBuilder.AlterColumn<int>(
                name: "OrderDetailsId",
                table: "Foods",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_OrderDetails_OrderDetailsId",
                table: "Foods",
                column: "OrderDetailsId",
                principalTable: "OrderDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
