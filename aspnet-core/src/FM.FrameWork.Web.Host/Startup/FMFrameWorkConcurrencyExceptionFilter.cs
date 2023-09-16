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
    public class FMFrameWorkConcurrencyExceptionFilter : IExceptionFilter, IFilterMetadata, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public FMFrameWorkConcurrencyExceptionFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void OnException(ExceptionContext context)
        {
            ILocalizationManager requiredService = _serviceProvider.GetRequiredService<ILocalizationManager>();
            FMFrameWorkConcurrencyExceptionOptions service = _serviceProvider.GetService<FMFrameWorkConcurrencyExceptionOptions>();
            service = service ?? new FMFrameWorkConcurrencyExceptionOptions
            {
                ConcurrencyMessage = new LocalizableString("ConcurrencyExceptionMessage", FMFrameWorkConsts.LocalizationSourceName)
            };
            string @string = requiredService.GetString(service.ConcurrencyMessage);
            AbpDbConcurrencyException ex = context.Exception as AbpDbConcurrencyException;
            if (ex != null)
            {
                context.Exception = new UserFriendlyException(@string, ex);
                return;
            }

            DbUpdateConcurrencyException ex2 = context.Exception as DbUpdateConcurrencyException;
            if (ex2 != null)
            {
                context.Exception = new UserFriendlyException(@string, ex2);
            }
        }
    }
}
