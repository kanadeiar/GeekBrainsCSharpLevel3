using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Task3.Data
{
    class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CinemaBoxDb>
    {
        public CinemaBoxDb CreateDbContext(string[] args)
        {
            //var options = new DbContextOptionsBuilder<CinemaBoxDb>()
            //    .UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CinemaBox.DB")
            //    .Options;
            return new CinemaBoxDb();
        }
    }
}
