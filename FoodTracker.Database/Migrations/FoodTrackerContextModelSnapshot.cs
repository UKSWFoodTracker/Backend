﻿// <auto-generated />
using FoodTracker.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FoodTracker.Database.Migrations
{
    [DbContext(typeof(FoodTrackerContext))]
    partial class FoodTrackerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FoodTracker.Model.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Calories");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("FoodTracker.Model.Meal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("FoodTracker.Model.MealIngredient", b =>
                {
                    b.Property<int>("IngredientId");

                    b.Property<int>("MealId");

                    b.HasKey("IngredientId", "MealId");

                    b.HasIndex("MealId");

                    b.ToTable("MealIngredients");
                });

            modelBuilder.Entity("FoodTracker.Model.MealIngredient", b =>
                {
                    b.HasOne("FoodTracker.Model.Ingredient", "Ingredient")
                        .WithMany("MealIngredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FoodTracker.Model.Meal", "Meal")
                        .WithMany("MealIngredients")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
