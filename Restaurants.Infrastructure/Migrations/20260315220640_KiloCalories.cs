using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class KiloCalories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Restaurants_RestaurantId1",
                table: "Dishes");

            migrationBuilder.DropIndex(
                name: "IX_Dishes_RestaurantId1",
                table: "Dishes");

            migrationBuilder.RenameColumn(
                name: "RestaurantId1",
                table: "Dishes",
                newName: "KiloCalories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KiloCalories",
                table: "Dishes",
                newName: "RestaurantId1");

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_RestaurantId1",
                table: "Dishes",
                column: "RestaurantId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Restaurants_RestaurantId1",
                table: "Dishes",
                column: "RestaurantId1",
                principalTable: "Restaurants",
                principalColumn: "Id");
        }
    }
}
