using squares_api_excercise.Models;
using System;

namespace squares_api_excercise.Data
{
    public class DataSeeder
    {
        public static async Task SeedAsync(SquaresDbContext context)
        {
            if (!context.Points.Any())
            {
                await context.Points.AddRangeAsync(new[]
                {
                    new Point { X = 0, Y = 0 },
                    new Point { X = 0, Y = 1 },
                    new Point { X = 1, Y = 0 },
                    new Point { X = 1, Y = 1 },
                    new Point { X = 2, Y = 2 },
                    new Point { X = 2, Y = 3 },

                });

                await context.SaveChangesAsync();
            }
        }
    }
}
