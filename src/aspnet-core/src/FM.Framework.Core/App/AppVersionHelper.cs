using Abp.Reflection.Extensions;

namespace FM.Framework.Core.App
{
    public class AppVersionHelper
    {
        //
        // 摘要:
        //     Gets current version of the application. It's also shown in the web page.
        public static string Version = "0.0.1";

        //
        // 摘要:
        //     Gets release (last build) date of the application. It's shown in the web page.
        public static DateTime ReleaseDate => new FileInfo(typeof(AppVersionHelper).GetAssembly().Location).LastWriteTime;
    }
}
