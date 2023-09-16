using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.UomModule.UOMService.Dtos
{
    /// <summary>
    /// 获取UOM编辑数据
    /// </summary>
    public class GetUomForEditDto
    {
        /// <summary>
        /// Uom实体数据
        /// </summary>
        public UomEditDto Uom { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string OperationName { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperationTime { get; set; }
    }
}
