using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineOrder.Db.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductSecond : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QTY",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QTY",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("5a6429bd-cc54-4252-a6ea-e370fcdada15"),
                column: "QTY",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("68e896f8-c272-4b92-b52d-10d83e6452a2"),
                column: "QTY",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("734b060e-7385-4c35-bfad-2187c5d8fd6c"),
                column: "QTY",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("86e210d8-3e55-4887-a957-55fa04bc7fc0"),
                column: "QTY",
                value: 0);
        }
    }
}
