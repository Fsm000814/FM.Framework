using Abp.Application.Services.Dto;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.Dto
{
    /// <summary>
    /// 支持分页、排序和FitlerText的Dto
    /// </summary>
    public class PagedSortedAndFilteredInputDto:IPagedResultRequest
    {
        /// <summary>
        /// 构造函数，设置初始值。
        /// </summary>
        public PagedSortedAndFilteredInputDto()
        {
            MaxResultCount = 10;
        }
        /// <summary>
        /// 筛选文本
        /// </summary>
        public virtual string FilterText { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual string Sorting { get; set; }

        /// <summary>
        /// 最大的返回条数
        /// </summary>
        [Range(1, 1000)]
        public virtual int MaxResultCount { get; set; }

        /// <summary>
        /// 跳过的数据量
        /// </summary>
        [Range(0, int.MaxValue)]
        public virtual int SkipCount { get; set; }
    }
}
