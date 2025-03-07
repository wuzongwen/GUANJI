# GUANJI

##  1.注意事项
#### 需要安装.NET8控制台运行时:[下载地址](https://dotnet.microsoft.com/zh-cn/download/dotnet/8.0)
#### 可以先不安装服务，直接执行GUANJI.exe允许控制台程序，测试是否可以正常连接MQTT
##  2.理论上可以跨平台，我只有Windows电脑的需求，其他平台自己打包测试

##  3.配置参数，有备注，自行修改
```
{
  "ServiceConfig": {
    "ServiceDescription": "这是一个使用MQTT服务控制电脑关机的服务", //服务描述
    "ServiceName": "MQTTShutdownService", //服务名称
    "MqttBroker": "your_mqtt_broker_address", //MQTT服务器地址
    "MqttPort": 1883, //MQTT服务器端口
    "MqttClientId": "RemoteShutdownService", //随便填
    "MqttTopic": "homeassistant/computer_control", //MQTT主题
    "MqttUserName": "your_mqtt_username", //MQTT用户名
    "MqttPassword": "your_mqtt_password", //MQTT密码
    "ShutdownMessage": "shutdown", //关机指令
    "TimeDelay": 1000 //关机延时，单位毫秒
  }
}
```

##  4.安装/卸载服务
####  Windows系统可以执行目录下的install.bat安装服务，执行uninstall.bat卸载服务

