using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Localization;
using Abp.UI;
using FM.Framework.Core.App;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Framework.Web.Host.ConcurrencyException
{
    public class ConcurrencyExceptionFilter : IExceptionFilter, IFilterMetadata, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public ConcurrencyExceptionFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void OnException(ExceptionContext context)
        {
            ILocalizationManager requiredService = _serviceProvider.GetRequiredService<ILocalizationManager>();
            ConcurrencyExceptionOptions service = _serviceProvider.GetService<ConcurrencyExceptionOptions>();
            service = service ?? new ConcurrencyExceptionOptions
            {
                ConcurrencyMessage = new LocalizableString("ConcurrencyExceptionMessage", AppConsts.LocalizationSourceName)
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
