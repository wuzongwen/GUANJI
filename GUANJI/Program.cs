using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Topshelf;
using static GUANJI.AppSetting;

namespace GUANJI
{
    internal class Program
    {
        static void Main()
        {
            // 使用 ConfigurationBuilder 加载 INI 配置
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // 将配置绑定到模型对象
            var serviceConfig = new ServiceConfig();
            configuration.GetSection("ServiceConfig").Bind(serviceConfig);

            // 设置依赖注入
            var services = new ServiceCollection();
            services.AddSingleton(serviceConfig);
            services.AddSingleton<ShutdownService>();  // RemoteShutdownService 会自动注入 MqttConfig

            var serviceProvider = services.BuildServiceProvider();

            HostFactory.Run(x =>
            {
                x.Service<ShutdownService>(s =>
                {
                    s.ConstructUsing(name => serviceProvider.GetService<ShutdownService>());
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());
                });

                x.RunAsLocalSystem();
                x.SetServiceName(serviceConfig.ServiceName);
                x.SetDisplayName(serviceConfig.ServiceName);
                x.SetDescription(serviceConfig.ServiceDescription);
            });
        }
    }
}
