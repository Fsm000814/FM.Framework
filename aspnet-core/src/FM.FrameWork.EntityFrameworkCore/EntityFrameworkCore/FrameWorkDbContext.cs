using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using FM.FrameWork.Authorization.Roles;
using FM.FrameWork.Authorization.Users;
using FM.FrameWork.MultiTenancy;

namespace FM.FrameWork.EntityFrameworkCore
{
    public class FrameWorkDbContext : AbpZeroDbContext<Tenant, Role, User, FrameWorkDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public FrameWorkDbContext(DbContextOptions<FrameWorkDbContext> options)
            : base(options)
        {
        }
    }
}
