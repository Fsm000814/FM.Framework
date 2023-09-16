using Microsoft.Extensions.Configuration;

namespace FM.FrameWork.Configuration
{
    /// <summary>
    /// 获取配置信息的工具接口
    /// </summary>
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
