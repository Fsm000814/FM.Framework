using System.Threading.Tasks;
using FM.FrameWork.Configuration.Dto;

namespace FM.FrameWork.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
