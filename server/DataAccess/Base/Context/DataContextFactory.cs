using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess.Base.Context
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                ?? throw new InvalidOperationException(
                    "DB_CONNECTION_STRING environment variable is not set. " +
                    "Set it before running EF Core migrations.");

            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseSqlServer(connectionString);
            return new DataContext(builder.Options);
        }
    }
}
