using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace any.Migrations
{
    /// <inheritdoc />
    public partial class lorem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Publishing_PublishingId",
                table: "Book");

            migrationBuilder.DropTable(
                name: "Publishing");

            migrationBuilder.DropIndex(
                name: "IX_Book_PublishingId",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "PublishingId",
                table: "Book");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublishingId",
                table: "Book",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Publishing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishing", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_PublishingId",
                table: "Book",
                column: "PublishingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Publishing_PublishingId",
                table: "Book",
                column: "PublishingId",
                principalTable: "Publishing",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
