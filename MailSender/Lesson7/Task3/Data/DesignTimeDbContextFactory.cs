using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Task3.Data
{
    class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CinemaBoxDb>
    {
        public CinemaBoxDb CreateDbContext(string[] args)
        {
            return new CinemaBoxDb();
        }
    }
}
