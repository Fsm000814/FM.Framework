using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.Dto
{
    public class CopyDto
    {
        /// <summary>
        /// 名称。
        /// </summary>
        [Required]
        public virtual string Name { get; set; }

        /// <summary>
        /// 处理参数
        /// </summary>
        public virtual void Normalize()
        {
            Name = Name.Trim();
        }
    }
}
