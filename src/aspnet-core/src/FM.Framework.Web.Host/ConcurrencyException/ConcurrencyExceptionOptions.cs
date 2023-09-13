using Abp.Localization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Framework.Web.Host.ConcurrencyException
{
    public class ConcurrencyExceptionOptions
    {
        public LocalizableString ConcurrencyMessage { get; set; }
    }
}
