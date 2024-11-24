using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace any.Migrations
{
    /// <inheritdoc />
    public partial class loretto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "User Id",
                table: "Cart",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_BookId",
                table: "Cart",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_User Id",
                table: "Cart",
                column: "User Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Book_BookId",
                table: "Cart",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_User_User Id",
                table: "Cart",
                column: "User Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Book_BookId",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_Cart_User_User Id",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_BookId",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_User Id",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "User Id",
                table: "Cart");
        }
    }
}
