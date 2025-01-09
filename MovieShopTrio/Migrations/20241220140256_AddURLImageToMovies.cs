using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieShopTrio.Migrations
{
    /// <inheritdoc />
    public partial class AddURLImageToMovies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "URLImage",
                table: "Movies",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "URLImage",
                table: "Movies");
        }
    }
}
