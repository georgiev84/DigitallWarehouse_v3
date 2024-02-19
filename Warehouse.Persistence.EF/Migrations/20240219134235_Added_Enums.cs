using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warehouse.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class Added_Enums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PaymentMethod",
                table: "Payments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "OrderStatus",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-2321-2321-111111111111"),
                column: "Name",
                value: 0);

            migrationBuilder.UpdateData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("22222222-1111-1234-4321-222222222222"),
                column: "Name",
                value: 1);

            migrationBuilder.UpdateData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3322-1122-4444-333333333333"),
                column: "Name",
                value: 2);

            migrationBuilder.UpdateData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("44444444-5555-5555-6666-666666666666"),
                column: "Name",
                value: 3);

            migrationBuilder.UpdateData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-8888-888888888888"),
                column: "Name",
                value: 4);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PaymentMethod",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OrderStatus",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-2321-2321-111111111111"),
                column: "Name",
                value: "Pending");

            migrationBuilder.UpdateData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("22222222-1111-1234-4321-222222222222"),
                column: "Name",
                value: "Processing");

            migrationBuilder.UpdateData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3322-1122-4444-333333333333"),
                column: "Name",
                value: "Shipped");

            migrationBuilder.UpdateData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("44444444-5555-5555-6666-666666666666"),
                column: "Name",
                value: "Completed");

            migrationBuilder.UpdateData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-8888-888888888888"),
                column: "Name",
                value: "Cancelled");
        }
    }
}
