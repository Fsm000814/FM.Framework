using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.UomModule.UOMService.Dtos
{
    /// <summary>
    /// Uom编辑对象
    /// </summary>
    public class UomEditDto
    {
        public Guid? Id { get; set; }
        /// <summary>
        /// Uom名称 
        /// </summary>
        public string UomName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
