using FoodTracker.Model;
using Microsoft.EntityFrameworkCore;

namespace FoodTracker.Database
{
    public class FoodTrackerContext : DbContext
    {
        public FoodTrackerContext(DbContextOptions<FoodTrackerContext> options) : base(options)
        { }

        public DbSet<Meal> Meals { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MealIngredient>()
                .HasKey(mi => new {mi.IngredientId, mi.MealId});

            modelBuilder.Entity<MealIngredient>()
                .HasOne(mi => mi.Ingredient)
                .WithMany(i => i.MealIngredients)
                .HasForeignKey(mi => mi.IngredientId);

            modelBuilder.Entity<MealIngredient>()
                .HasOne(mi => mi.Meal)
                .WithMany(m => m.MealIngredients)
                .HasForeignKey(mi => mi.MealId);
        }
    }
}
