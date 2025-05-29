using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineOrder.Db.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFieldNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("5a6429bd-cc54-4252-a6ea-e370fcdada15"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("68e896f8-c272-4b92-b52d-10d83e6452a2"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("734b060e-7385-4c35-bfad-2187c5d8fd6c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("86e210d8-3e55-4887-a957-55fa04bc7fc0"));

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Orders",
                newName: "DataCreatedOrder");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Orders",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "DataCreatedOrder",
                table: "Orders",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Orders",
                newName: "OrderId");

            migrationBuilder.InsertData(
                table: "Products",
                column: "ProductId",
                values: new object[]
                {
                    new Guid("5a6429bd-cc54-4252-a6ea-e370fcdada15"),
                    new Guid("68e896f8-c272-4b92-b52d-10d83e6452a2"),
                    new Guid("734b060e-7385-4c35-bfad-2187c5d8fd6c"),
                    new Guid("86e210d8-3e55-4887-a957-55fa04bc7fc0")
                });
        }
    }
}
