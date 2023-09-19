using Abp.Localization;

namespace FM.FrameWork.Web.Host.Startup
{
    /// <summary>
    /// 并发异常选项
    /// </summary>
    public class FMFrameWorkConcurrencyExceptionOptions
    {
        /// <summary>
        /// 并发消息本地多语言化配置
        /// </summary>
        public LocalizableString ConcurrencyMessage { get; set; }
    }
}
