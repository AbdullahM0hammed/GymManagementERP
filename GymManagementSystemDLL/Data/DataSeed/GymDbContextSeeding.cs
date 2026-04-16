using GymManagementSystemDAL.Data.Contexts;
using GymManagementSystemDAL.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace GymManagementSystemDAL.Data.DataSeed
{
    public static class GymDbContextSeeding
    {
        public static bool SeedData(GymDbContext context)
        {
            try
            {
                var HasPlans = context.Plans.Any();
                var HasCategories = context.Categories.Any();
                if (HasPlans && HasPlans) return false;
                if (!HasPlans)
                {
                    var Plans = LoadDataFromFile<Plan>("plans.json");
                    if (Plans.Any())
                        context.Plans.AddRange(Plans);
                }
                if (!HasCategories)
                {
                    var categori = LoadDataFromFile<Category>("categories.json");
                    if (categori.Any())
                        context.Categories.AddRange(categori);
                }
                return context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding data: {ex.Message}");
                return false;
            }
        }
        private static List<T> LoadDataFromFile<T>(string fileName)
        {
            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "Files",
                fileName
            );

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File not found: {fileName}");

            string data = File.ReadAllText(filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<List<T>>(data, options) ?? new List<T>();
        }
    }
}
