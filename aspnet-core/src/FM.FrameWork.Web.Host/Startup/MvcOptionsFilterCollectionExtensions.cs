using Abp;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FM.FrameWork.Web.Host.Startup
{
    public static class MvcOptionsFilterCollectionExtensions
    {
        public static FilterCollection Insert<TFilter>(this FilterCollection filters, int index) where TFilter : IFilterMetadata
        {
            return filters.Insert(index, typeof(TFilter));
        }

        public static FilterCollection Insert(this FilterCollection filters, int index, Type filterType)
        {
            Check.NotNull(filterType, "filterType");
            if (!typeof(IFilterMetadata).IsAssignableFrom(filterType))
            {
                throw new ArgumentException("请输入正确的 fitler 类型");
            }

            ServiceFilterAttribute item = new ServiceFilterAttribute(filterType);
            filters.Insert(index, item);
            return filters;
        }

        public static int IndexOf<TFilter>(this FilterCollection filters) where TFilter : IFilterMetadata
        {
            return filters.IndexOf(typeof(TFilter));
        }

        public static int IndexOf(this FilterCollection filters, Type filterType)
        {
            int num = 0;
            foreach (IFilterMetadata filter in filters)
            {
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
