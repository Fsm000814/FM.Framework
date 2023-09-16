using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.UomModule.UOMService.Dtos
{
    /// <summary>
    /// 创建或修改UOM Input对象
    /// </summary>
    public class CreateOrEditUomInput
    {
        /// <summary>
        /// Uom对象数据
        /// </summary>
        [Required]
        public UomEditDto Uom { get; set; }
    }
}
