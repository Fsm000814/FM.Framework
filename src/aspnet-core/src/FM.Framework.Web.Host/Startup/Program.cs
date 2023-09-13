using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using FM.Framework.Web.Host.Startup;
using FM.Framework.Core.ConfigurationHelper;
using Serilog;
using System;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = CreateSerilogLogger();
        try
        {
            BuildWebHost(args).Run();
        }
        catch (Exception ex)
        {
            Log.Fatal("系统启动失败...", ex.StackTrace);
            Log.Fatal(ex.StackTrace);
        }
        finally
        {
            Log.CloseAndFlush();
        }

    }
    public static IWebHost BuildWebHost(string[] args)
    {
        var configuration = ConfigurationHelper.GetConfiguration("appsettings");
        int.TryParse(configuration["App:WebHostPort"], out var port);
        port = port == default ? 80 : port;

        return WebHost.CreateDefaultBuilder(args)
             //配置Kestrel服务器，通过ConfigureKestrel方法设置了最大请求体大小为300MB。
             .ConfigureKestrel((context, options) =>
             {
                 options.Limits.MaxRequestBodySize = 1024 * 1024 * 300;
             })
             //使用UseIIS和UseIISIntegration方法配置应用程序与IIS集成。
            .UseIIS()
            .UseIISIntegration()
            //使用UseStartup方法指定启动类为Startup。
            .UseStartup<Startup>()
            //使用UseKestrel方法监听指定端口号的任意IP地址
            .UseKestrel(op => op.ListenAnyIP(port))
            //使用UseSerilog方法配置日志记录器
            .UseSerilog()
            //调用Build方法构建WebHost实例
            .Build();
    }
    #region 日志配置

    /// <summary>
    /// 配置 SerilogLogger 配置
    /// </summary>
    /// <returns></returns>
    private static Serilog.ILogger CreateSerilogLogger()
    {
        string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        // 获取日志配置文件
        var configuration = ConfigurationHelper.GetConfiguration("serilog", environmentName: env);

        // 构建日志对象
        var cfg = new LoggerConfiguration()
            .ReadFrom
            .Configuration(configuration);

        return cfg.CreateLogger();
    }

    #endregion 日志配置
}