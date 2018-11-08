using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodTracker.Database.Migrations
{
    public partial class MealIngredients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealIngredient_Ingredients_IngredientId",
                table: "MealIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_MealIngredient_Meals_MealId",
                table: "MealIngredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealIngredient",
                table: "MealIngredient");

            migrationBuilder.RenameTable(
                name: "MealIngredient",
                newName: "MealIngredients");

            migrationBuilder.RenameIndex(
                name: "IX_MealIngredient_MealId",
                table: "MealIngredients",
                newName: "IX_MealIngredients_MealId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealIngredients",
                table: "MealIngredients",
                columns: new[] { "IngredientId", "MealId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MealIngredients_Ingredients_IngredientId",
                table: "MealIngredients",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealIngredients_Meals_MealId",
                table: "MealIngredients",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealIngredients_Ingredients_IngredientId",
                table: "MealIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_MealIngredients_Meals_MealId",
                table: "MealIngredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealIngredients",
                table: "MealIngredients");

            migrationBuilder.RenameTable(
                name: "MealIngredients",
                newName: "MealIngredient");

            migrationBuilder.RenameIndex(
                name: "IX_MealIngredients_MealId",
                table: "MealIngredient",
                newName: "IX_MealIngredient_MealId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealIngredient",
                table: "MealIngredient",
                columns: new[] { "IngredientId", "MealId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MealIngredient_Ingredients_IngredientId",
                table: "MealIngredient",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealIngredient_Meals_MealId",
                table: "MealIngredient",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
