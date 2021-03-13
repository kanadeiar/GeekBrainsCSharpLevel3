using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Task3.Models;

namespace Task3.Data
{
    class CinemaBoxDb : DbContext
    {
        private static Random rnd = new Random();
        public DbSet<MovieShow> MovieShows { get; set; }
        public DbSet<Order> Orders { get; set; }
        public CinemaBoxDb() : base()
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CinemaBox.DB");
        }
        public static void InitDb(CinemaBoxDb db)
        {
            if (!db.MovieShows.Any())
            {
                var movieShows = Enumerable.Range(1, 10).Select(i =>
                    new MovieShow
                    {
                        BeginTime = DateTime.Now,
                        Name = $"Киносеанс с фильмом № {i}",
                    }).ToArray();
                db.MovieShows.AddRange(movieShows);
                db.SaveChanges();
                if (!db.Orders.Any())
                {
                    var orders = Enumerable.Range(1, 20).Select(i =>
                        new Order
                        {
                            DateTime = DateTime.Now.AddHours(-2),
                            Count = rnd.Next(1,5),
                            MovieShow = movieShows[rnd.Next(movieShows.Length)],
                        }).ToArray();
                    db.Orders.AddRange(orders);
                    db.SaveChanges();
                }
            }
        }
    }
}
