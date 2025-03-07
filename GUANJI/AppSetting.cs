using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUANJI
{
    public class AppSetting
    {
        public class ServiceConfig
        {
            public string ServiceName { get; set; }
            public string ServiceDescription { get; set; }
            public string MqttBroker { get; set; }
            public int MqttPort { get; set; }
            public string MqttClientId { get; set; }
            public string MqttTopic { get; set; }
            public string MqttUserName { get; set; }
            public string MqttPassword { get; set; }
            public string ShutdownMessage { get; set; }
            public int TimeDelay { get; set; }
        }
    }
}
