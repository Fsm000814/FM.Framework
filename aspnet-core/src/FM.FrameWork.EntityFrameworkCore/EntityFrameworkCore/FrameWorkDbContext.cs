using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using FM.FrameWork.Authorization.Roles;
using FM.FrameWork.Authorization.Users;
using FM.FrameWork.MultiTenancy;
using FM.FrameWork.UomModule.UOMDefinition;

namespace FM.FrameWork.EntityFrameworkCore
{
    public class FrameWorkDbContext : FrameWorkDbContextBase<Tenant, Role, User, FrameWorkDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public FrameWorkDbContext(DbContextOptions<FrameWorkDbContext> options)
            : base(options)
        {

        }

        

        public override void ModelCreating(ModelBuilder modelBuilder)
        {

        }

        /// <summary>
        /// 度量单位
        /// </summary>
        public DbSet<UOM> UOM { get; set; }
    }
}
