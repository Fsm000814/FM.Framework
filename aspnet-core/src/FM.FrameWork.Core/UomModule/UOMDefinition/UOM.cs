using Abp.Domain.Entities;

using FM.FrameWork.Entities.Auditing;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.UomModule.UOMDefinition
{
    /// <summary>
    /// 度量单位
    /// </summary>
    [Comment("度量单位")]
    public class UOM : FullAuditedEntity<Guid>,IMayHaveTenant
    {
        public const int MaxUOMNameLength = 200;
        public const int MaxDescriptionLength = 1000;
        /// <summary>
        /// 度量单位的名称，不能重复
        /// </summary>
        [MaxLength(MaxUOMNameLength)]
        [Required]
        [Comment("度量单位的名称，不能重复")]
        public string UOMName { get; set; }

        /// <summary>
        /// 租户
        /// </summary>
        public int? TenantId { get; set; }
    }
}
