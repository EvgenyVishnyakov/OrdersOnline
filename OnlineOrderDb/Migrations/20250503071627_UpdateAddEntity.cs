using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineOrder.Db.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAddEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Orders_OrderId",
                table: "OrderProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Products_ProductId",
                table: "OrderProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_Orderid",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_Orderid",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProduct",
                table: "OrderProduct");

            migrationBuilder.DropColumn(
                name: "Orderid",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "OrderProduct",
                newName: "OrderProducts");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Orders",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProduct_ProductId",
                table: "OrderProducts",
                newName: "IX_OrderProducts_ProductId");

            migrationBuilder.AddColumn<int>(
                name: "ProductCount",
                table: "OrderProducts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts",
                columns: new[] { "OrderId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "ProductCount",
                table: "OrderProducts");

            migrationBuilder.RenameTable(
                name: "OrderProducts",
                newName: "OrderProduct");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Orders",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProduct",
                newName: "IX_OrderProduct_ProductId");

            migrationBuilder.AddColumn<Guid>(
                name: "Orderid",
                table: "Products",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProduct",
                table: "OrderProduct",
                columns: new[] { "OrderId", "ProductId" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: new Guid("5a6429bd-cc54-4252-a6ea-e370fcdada15"),
                column: "Orderid",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: new Guid("68e896f8-c272-4b92-b52d-10d83e6452a2"),
                column: "Orderid",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: new Guid("734b060e-7385-4c35-bfad-2187c5d8fd6c"),
                column: "Orderid",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: new Guid("86e210d8-3e55-4887-a957-55fa04bc7fc0"),
                column: "Orderid",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Orderid",
                table: "Products",
                column: "Orderid");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Orders_OrderId",
                table: "OrderProduct",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Products_ProductId",
                table: "OrderProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_Orderid",
                table: "Products",
                column: "Orderid",
                principalTable: "Orders",
                principalColumn: "id");
        }
    }
}
