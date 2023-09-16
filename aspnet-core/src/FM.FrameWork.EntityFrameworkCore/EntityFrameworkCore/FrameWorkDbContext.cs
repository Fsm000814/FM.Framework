using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using FM.FrameWork.Authorization.Roles;
using FM.FrameWork.Authorization.Users;
using FM.FrameWork.MultiTenancy;
using FM.FrameWork.UomModule.UOMDefinition;
using FM.FrameWork.Database;

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
            #region 分数据库处理

            switch (DatabaseInfo.Instance.DatabaseType)
            {
                case DatabaseTypeEnum.PostgreSQL:
                    break;

                case DatabaseTypeEnum.MySQL:
                    break;

                case DatabaseTypeEnum.Oracle:
                    //modelBuilder.LowCodeOracleEntityMapper();
                    //modelBuilder.MedProUseOracleTableMapping();
                    //modelBuilder.MedProOracleEntityMapper();
                    //modelBuilder.MedProEntityMapping();
                    break;

                case DatabaseTypeEnum.SqlServer:
                    break;
            }

            #endregion 分数据库处理
        }

        /// <summary>
        /// 度量单位
        /// </summary>
        public DbSet<UOM> UOM { get; set; }
    }
}
