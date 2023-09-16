using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.Dto
{
    /// <summary>
    /// Ndo数据传输对象
    /// </summary>
    public class NdoDto
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 自定义数据,根据业务进行扩展
        /// </summary>
        public virtual object CustomData { get; set; }
    }
}
