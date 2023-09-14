using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace FM.FrameWork.EntityFrameworkCore
{
    public static class FrameWorkDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<FrameWorkDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<FrameWorkDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
