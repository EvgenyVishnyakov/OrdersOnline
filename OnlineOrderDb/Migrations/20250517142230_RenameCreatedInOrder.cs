using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineOrder.Db.Migrations
{
    /// <inheritdoc />
    public partial class RenameCreatedInOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "created",
                table: "Orders",
                newName: "Created");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Orders",
                newName: "created");
        }
    }
}
