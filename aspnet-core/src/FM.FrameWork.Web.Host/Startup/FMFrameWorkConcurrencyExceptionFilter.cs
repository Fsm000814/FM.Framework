using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Localization;
using Abp.UI;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System;

namespace FM.FrameWork.Web.Host.Startup
{
    /// <summary>
    /// 并发异常筛选器
    /// </summary>
    public class FMFrameWorkConcurrencyExceptionFilter : IExceptionFilter, IFilterMetadata, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 并发异常筛选器构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        public FMFrameWorkConcurrencyExceptionFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 在发生异常时调用的方法
        /// </summary>
        /// <param name="context">异常上下文</param>
        public void OnException(ExceptionContext context)
        {
            // 获取 ILocalizationManager 的实例
            ILocalizationManager requiredService = _serviceProvider.GetRequiredService<ILocalizationManager>();

            // 获取 FMFrameWorkConcurrencyExceptionOptions 的实例
            FMFrameWorkConcurrencyExceptionOptions service = _serviceProvider.GetService<FMFrameWorkConcurrencyExceptionOptions>();

            // 如果 service 为 null，则使用默认的 FMFrameWorkConcurrencyExceptionOptions 实例，并设置 ConcurrencyMessage 字段
            service = service ?? new FMFrameWorkConcurrencyExceptionOptions
            {
                ConcurrencyMessage = new LocalizableString("ConcurrencyExceptionMessage", FMFrameWorkConsts.LocalizationSourceName)
            };

            // 根据 ConcurrencyMessage 获取本地化字符串
            string @string = requiredService.GetString(service.ConcurrencyMessage);

            // 检查异常类型是否为 AbpDbConcurrencyException
            AbpDbConcurrencyException ex = context.Exception as AbpDbConcurrencyException;
            if (ex != null)
            {
                // 将异常转换为 UserFriendlyException，并用本地化字符串作为消息
                context.Exception = new UserFriendlyException(@string, ex);
                return;
            }

            // 检查异常类型是否为 DbUpdateConcurrencyException
            DbUpdateConcurrencyException ex2 = context.Exception as DbUpdateConcurrencyException;
            if (ex2 != null)
            {
                // 将异常转换为 UserFriendlyException，并用本地化字符串作为消息
                context.Exception = new UserFriendlyException(@string, ex2);
            }
        }
    }
}
