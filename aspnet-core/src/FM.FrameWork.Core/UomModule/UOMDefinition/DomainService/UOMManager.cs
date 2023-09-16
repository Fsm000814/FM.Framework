using FM.FrameWork.Ndo.GuidNdoDomainService;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.UomModule.UOMDefinition.DomainService
{
    public class UOMManager : GuidNdoDomainService<UOM>, IUOMManager
    {
        public UOMManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override IQueryable<UOM> GetNdoIncludeQuery()
        {
            return Query.AsNoTracking();
        }

        /// <summary>
        /// 添加或修改校验
        /// </summary>
        /// <param name="ndo"></param>
        /// <returns></returns>
        public override async Task ValidateNdoOnCreateOrUpdate(UOM ndo)
        {
            //名称不可重复校验
            var count = await Query .Where(a => a.UOMName == ndo.UOMName && a.Id != ndo.Id).CountAsync();
            if (count > 0)
            {
                this.ThrowRepetError(ndo.UOMName);
            }
        }

        /// <summary>
        /// 校验是否可以删除
        /// </summary>
        /// <param name="ndo"></param>
        /// <returns></returns>
        public override async Task ValidateNdoOnDelete(UOM ndo)
        {
            if (ndo == null)
            {
                return;
            }
        }
    }
}
