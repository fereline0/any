using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace any.Migrations
{
    /// <inheritdoc />
    public partial class doraty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_User_User Id",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_User Id",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "User Id",
                table: "Cart");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId1",
                table: "Book",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserId",
                table: "Cart",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorId1",
                table: "Book",
                column: "AuthorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Author_AuthorId1",
                table: "Book",
                column: "AuthorId1",
                principalTable: "Author",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_User_UserId",
                table: "Cart",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Author_AuthorId1",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Cart_User_UserId",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_UserId",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Book_AuthorId1",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "AuthorId1",
                table: "Book");

            migrationBuilder.AddColumn<int>(
                name: "User Id",
                table: "Cart",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_User Id",
                table: "Cart",
                column: "User Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_User_User Id",
                table: "Cart",
                column: "User Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
