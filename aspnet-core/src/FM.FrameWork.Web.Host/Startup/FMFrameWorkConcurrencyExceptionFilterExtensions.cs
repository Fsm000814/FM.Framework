using Abp.AspNetCore.Mvc.ExceptionHandling;
using Abp.Localization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FM.FrameWork.Web.Host.Startup
{
    /// <summary>
    /// 并发异常筛选器扩展类
    /// </summary>
    public static class FMFrameWorkConcurrencyExceptionFilterExtensions
    {
        /// <summary>
        /// 添加并发异常Filter
        /// </summary>
        /// <param name="mvcOptions"></param>
        /// <returns></returns>
        public static MvcOptions AddConcurrencyExceptionFilter(this MvcOptions mvcOptions)
        {
            int num = mvcOptions.Filters.IndexOf<AbpExceptionFilter>();
            mvcOptions.Filters.Insert<FMFrameWorkConcurrencyExceptionFilter>(num + 1);
            return mvcOptions;
        }

        /// <summary>
        /// 配置并发异常
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configAction">配置行为</param>
        /// <returns>配置后的服务集合</returns>
        public static IServiceCollection ConfigConcurrencyException(this IServiceCollection services, Action<FMFrameWorkConcurrencyExceptionOptions> configAction)
        {
            // 创建一个 并发异常选项 实例
            FMFrameWorkConcurrencyExceptionOptions gctConcurrencyExceptionOptions = new FMFrameWorkConcurrencyExceptionOptions();

            // 执行传入的 configAction 方法，并将 gctConcurrencyExceptionOptions 作为参数传递
            configAction?.Invoke(gctConcurrencyExceptionOptions);

            // 如果并发消息本地多语言化配置为 null，则使用默认的本地化字符串
            gctConcurrencyExceptionOptions.ConcurrencyMessage = gctConcurrencyExceptionOptions.ConcurrencyMessage ?? new LocalizableString("ConcurrencyExceptionMessage", FMFrameWorkConsts.LocalizationSourceName);

            // 注册 gctConcurrencyExceptionOptions 为单例服务
            services.AddSingleton(gctConcurrencyExceptionOptions);

            // 返回更新后的服务集合
            return services;
        }
    }
}
