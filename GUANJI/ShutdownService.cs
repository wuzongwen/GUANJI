using Microsoft.Extensions.Configuration;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Protocol;
using NLog;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static GUANJI.AppSetting;

namespace GUANJI
{
    public class ShutdownService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private IMqttClient mqttClient;
        private ServiceConfig serviceConfig;
        private Timer networkTimer;

        public ShutdownService(ServiceConfig config)
        {
            serviceConfig = config;
            var factory = new MqttFactory();
            mqttClient = factory.CreateMqttClient();
        }

        public void Start()
        {
            Logger.Info("服务启动");
            // 启动定时器，立即开始，间隔10秒（10000毫秒）
            networkTimer = new Timer(NetworkCheck, null, 0, 10000);
        }

        public void Stop()
        {
            Logger.Info("服务停止");
            mqttClient?.DisconnectAsync().Wait();
        }

        private async Task ConnectMqttClient()
        {
            var mqttFactory = new MqttFactory();
            mqttClient = mqttFactory.CreateMqttClient();

            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(serviceConfig.MqttBroker, serviceConfig.MqttPort)
                .WithCredentials(serviceConfig.MqttUserName, serviceConfig.MqttPassword)
                .WithClientId(serviceConfig.MqttPassword)
                .Build();

            mqttClient.ApplicationMessageReceivedAsync += e =>
            {
                var message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                Console.WriteLine("接收到MQTT消息：{0}", message);
                Logger.Info("接收到MQTT消息: {0}", message);
                if (message == serviceConfig.ShutdownMessage)
                {
                    Logger.Info("接收到关机指令");
                    ShutdownComputer();
                }
                return Task.CompletedTask;
            };

            mqttClient.ConnectedAsync += async e =>
            {
                Console.WriteLine("MQTT服务已连接");
                Logger.Info("MQTT服务已连接");
                await mqttClient.SubscribeAsync(serviceConfig.MqttTopic);
            };

            await mqttClient.ConnectAsync(mqttClientOptions);
        }

        private void ShutdownComputer()
        {
            try
            {
                Logger.Info("执行关机指令");
                Process.Start("shutdown", $"/s /t {serviceConfig.TimeDelay}");
                Logger.Info("关机指令执行成功");
            }
            catch (Exception ex)
            {
                Logger.Info($"关机指令执行失败,{JsonSerializer.Serialize(ex)}");
            }
        }

        /// <summary>
        /// 定时器回调：检测网络状态
        /// </summary>
        private void NetworkCheck(object state)
        {
            if (IsNetworkAvailable(out IPAddress ip))
            {
                Logger.Info("网络检测通过，本机IP地址：{0}", ip);
                // 网络就绪，停止并释放定时器
                networkTimer?.Change(Timeout.Infinite, Timeout.Infinite);
                networkTimer?.Dispose();
                networkTimer = null;
                // 开始连接MQTT服务
                Task.Run(() => ConnectMqttClient());
            }
            else
            {
                Logger.Info("网络未就绪，10秒后重试...");
            }
        }

        /// <summary>
        /// 检测本机是否有网络连接且获取有效IPv4地址
        /// </summary>
        private bool IsNetworkAvailable(out IPAddress localIp)
        {
            localIp = null;
            if (!NetworkInterface.GetIsNetworkAvailable())
                return false;
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                localIp = host.AddressList.FirstOrDefault(ip =>
                    ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && !IPAddress.IsLoopback(ip));
                return localIp != null;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "检测网络状态时发生异常");
                return false;
            }
        }
    }
}
