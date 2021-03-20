using Microsoft.EntityFrameworkCore;
using Task4.Model;

namespace Task4.Data
{
    class PersonDB : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public PersonDB() : base()
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Persons.DB");
        }
    }
}
