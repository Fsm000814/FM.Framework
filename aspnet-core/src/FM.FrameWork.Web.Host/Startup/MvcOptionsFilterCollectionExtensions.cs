using Abp;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FM.FrameWork.Web.Host.Startup
{
    /// <summary>
    /// Mvc选项筛选器集合扩展
    /// </summary>
    public static class MvcOptionsFilterCollectionExtensions
    {
        /// <summary>
        /// 将过滤器类型插入到过滤器集合中的指定位置
        /// </summary>
        /// <typeparam name="TFilter">过滤器的类型</typeparam>
        /// <param name="filters">进行插入操作的 FilterCollection 对象</param>
        /// <param name="index">插入的位置索引</param>
        /// <returns></returns>
        public static FilterCollection Insert<TFilter>(this FilterCollection filters, int index) where TFilter : IFilterMetadata
        {
            return filters.Insert(index, typeof(TFilter));
        }

        /// <summary>
        /// 将过滤器类型插入到过滤器集合中的指定位置
        /// </summary>
        /// <param name="filters">进行插入操作的 FilterCollection 对象</param>
        /// <param name="index">插入的位置索引</param>
        /// <param name="filterType">过滤器的类型</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static FilterCollection Insert(this FilterCollection filters, int index, Type filterType)
        {
            // 检查过滤器类型是否为空
            Check.NotNull(filterType, "filterType");
            // 判断过滤器类型是否实现了 IFilterMetadata 接口
            if (!typeof(IFilterMetadata).IsAssignableFrom(filterType))
            {
                throw new ArgumentException("请输入正确的 filter 类型");
            }

            // 创建一个 ServiceFilterAttribute 对象，将过滤器类型作为参数传入
            ServiceFilterAttribute item = new ServiceFilterAttribute(filterType);

            // 在指定位置插入创建的过滤器对象到过滤器集合中
            filters.Insert(index, item);

            // 返回更新后的过滤器集合
            return filters;
        }

        /// <summary>
        /// 获取指定过滤器类型在过滤器集合中的索引位置
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static int IndexOf<TFilter>(this FilterCollection filters) where TFilter : IFilterMetadata
        {
            return filters.IndexOf(typeof(TFilter));
        }

        /// <summary>
        /// 该方法用于获取指定过滤器类型在过滤器集合中的索引位置
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="filterType"></param>
        /// <returns></returns>
        public static int IndexOf(this FilterCollection filters, Type filterType)
        {
            //定义当前索引位置
            int num = 0;
            foreach (IFilterMetadata filter in filters)
            {
                //判断当前过滤器的类型是否与目标过滤器类型相同 
                if (!(filter.GetType() == filterType))
                {
                    ServiceFilterAttribute serviceFilterAttribute = filter as ServiceFilterAttribute;
                    if (serviceFilterAttribute == null || !(serviceFilterAttribute.ServiceType == filterType))
                    {
                        num++;
                        continue;
                    }

                    return num;
                }

                return num;
            }

            return num;
        }
    }
}
