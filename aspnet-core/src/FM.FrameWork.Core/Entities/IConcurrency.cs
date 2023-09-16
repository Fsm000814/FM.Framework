using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.Entities
{
    /// <summary>
    /// 并发实体接口
    /// </summary>
    public interface IConcurrency
    {
        /// <summary>
        ///  并发令牌，最长32位
        /// </summary>
        [MaxLength(32)]
        string ConcurrencyToken { get; set; }
    }
}
