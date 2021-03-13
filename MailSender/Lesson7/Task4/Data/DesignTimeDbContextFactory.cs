using Microsoft.EntityFrameworkCore.Design;

namespace Task4.Data
{
    class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PersonDB>
    {
        public PersonDB CreateDbContext(string[] args)
        {
            return new PersonDB();
        }
    }
}
