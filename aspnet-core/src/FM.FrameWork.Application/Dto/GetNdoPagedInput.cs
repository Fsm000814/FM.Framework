using Abp.Runtime.Validation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.Dto
{
    /// <summary>
    /// Ndo分页获取Input
    /// </summary>
    public class GetNdoPagedInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {
        /// <summary>
        /// 正常化排序使用
        /// </summary>
        public virtual void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = FMFrameWorkConfigs.Dto.NdoSortByName;
            }
        }
    }
}
