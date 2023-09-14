using FM.FrameWork.Debugging;

namespace FM.FrameWork
{
    public class FrameWorkConsts
    {
        public const string LocalizationSourceName = "FrameWork";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "3c554a6edf0b4157a588e1b98d303c93";
    }
}
