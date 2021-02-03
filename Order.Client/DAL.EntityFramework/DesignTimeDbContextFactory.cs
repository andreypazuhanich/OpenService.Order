using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DAL.EntityFramework
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseSqlServer("Server=ANDREY;Database=OrderDB;Trusted_Connection=True;MultipleActiveResultSets=True;");
            return new AppDbContext(builder.Options);
        }
    }
}