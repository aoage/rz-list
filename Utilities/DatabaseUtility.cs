using Microsoft.EntityFrameworkCore;
using Data;

namespace rz_list.Utilities
{
    public static class DatabaseUtility
    {
        public static async Task SeedDatabase(IDbContextFactory<RzListDbContext> dbFactory)
        {
            using var context = dbFactory.CreateDbContext();
            await DataSeeder.SeedDatabase(context);
        }

        public static async Task ClearAndReseedDatabase(IDbContextFactory<RzListDbContext> dbFactory)
        {
            using var context = dbFactory.CreateDbContext();
            await DataSeeder.ClearAndReseedDatabase(context);
        }

        public static async Task<DatabaseStats> GetDatabaseStats(IDbContextFactory<RzListDbContext> dbFactory)
        {
            using var context = dbFactory.CreateDbContext();

            var totalBooks = await context.Books.CountAsync();
            var totalGenres = await context.Books.Select(b => b.Genre).Distinct().CountAsync();
            var totalPublishers = await context.Books
                .Where(b => !string.IsNullOrEmpty(b.Publisher))
                .Select(b => b.Publisher)
                .Distinct()
                .CountAsync();
            var totalAuthors = await context.Books.Select(b => b.Author).Distinct().CountAsync();

            return new DatabaseStats
            {
                TotalBooks = totalBooks,
                TotalGenres = totalGenres,
                TotalPublishers = totalPublishers,
                TotalAuthors = totalAuthors
            };
        }
    }

    public class DatabaseStats
    {
        public int TotalBooks { get; set; }
        public int TotalGenres { get; set; }
        public int TotalPublishers { get; set; }
        public int TotalAuthors { get; set; }

        public override string ToString()
        {
            return $"Database Stats:\n" +
                   $"  Total Books: {TotalBooks}\n" +
                   $"  Total Genres: {TotalGenres}\n" +
                   $"  Total Publishers: {TotalPublishers}\n" +
                   $"  Total Authors: {TotalAuthors}";
        }
    }
}
