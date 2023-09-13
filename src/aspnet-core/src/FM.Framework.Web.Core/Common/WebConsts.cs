namespace FM.Framework.Web.Core.Common
{
    public static class WebConsts
    {
        public static class GraphQL
        {
            public const string PlaygroundEndPoint = "/ui/playground";

            public const string EndPoint = "/graphql";

            public static bool PlaygroundEnabled;

            public static bool Enabled;
        }

        public static class MinioConfig
        {
            public static bool IsEnabled = false;

            public static string Endpoint = "";

            public static string AccessKey = "";

            public static string SecretKey = "";

            public static string BucketName = "";
        }

        public const string SwaggerUiEndPoint = "/swagger";

        public const string HangfireDashboardEndPoint = "/hangfire";

        public const string HealthCheckEndPoint = "/healthz";

        public const string HealthCheckUIEndPoint = "/healthchecks-ui";

        public static bool SwaggerUiEnabled = true;

        public static bool HealthCheckEnabled = true;

        public static bool HealthCheckUIEnabled = true;

        public static int RedisDatabaseId = -1;

        public static string RedisConnectionStr = "";

        public static int SignalRDatabaseId = -1;

        public static string ConnectionSignalRString = "";

        //
        // 摘要:
        //     是否启用hangfir面板
        public static bool HangfireDashboardEnabled = true;

        public static bool HangfireEnabled = true;

        public const string DefaultCorsPolicyName = "FMFramework";
    }
}