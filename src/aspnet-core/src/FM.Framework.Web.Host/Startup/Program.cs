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
            Log.Fatal("ϵͳ����ʧ��...", ex.StackTrace);
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
             //����Kestrel��������ͨ��ConfigureKestrel��������������������СΪ300MB��
             .ConfigureKestrel((context, options) =>
             {
                 options.Limits.MaxRequestBodySize = 1024 * 1024 * 300;
             })
             //ʹ��UseIIS��UseIISIntegration��������Ӧ�ó�����IIS���ɡ�
            .UseIIS()
            .UseIISIntegration()
            //ʹ��UseStartup����ָ��������ΪStartup��
            .UseStartup<Startup>()
            //ʹ��UseKestrel��������ָ���˿ںŵ�����IP��ַ
            .UseKestrel(op => op.ListenAnyIP(port))
            //ʹ��UseSerilog����������־��¼��
            .UseSerilog()
            //����Build��������WebHostʵ��
            .Build();
    }
    #region ��־����

    /// <summary>
    /// ���� SerilogLogger ����
    /// </summary>
    /// <returns></returns>
    private static Serilog.ILogger CreateSerilogLogger()
    {
        string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        // ��ȡ��־�����ļ�
        var configuration = ConfigurationHelper.GetConfiguration("serilog", environmentName: env);

        // ������־����
        var cfg = new LoggerConfiguration()
            .ReadFrom
            .Configuration(configuration);

        return cfg.CreateLogger();
    }

    #endregion ��־����
}