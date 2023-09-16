using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.Dto
{
    /// <summary>
    /// 复制Ndo数据传输对象
    /// </summary>
    public class CopyNdoDto:CopyDto
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual Guid Id { get; set; }
    }
}
