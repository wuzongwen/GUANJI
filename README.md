# GUANJI
使用MQTT服务控制PC关机
##  1.注意事项
#### 需要安装.NET8控制台运行时:[下载地址](https://dotnet.microsoft.com/zh-cn/download/dotnet/8.0)
#### 可以先不安装服务，直接执行GUANJI.exe允许控制台程序，测试是否可以正常连接MQTT

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

##  5.控制台运行效果
![输入图片说明](https://private-user-images.githubusercontent.com/16460092/420283485-1f286f92-9957-4a93-8624-80bace221cba.png?jwt=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJnaXRodWIuY29tIiwiYXVkIjoicmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbSIsImtleSI6ImtleTUiLCJleHAiOjE3NDEzNDEzMDMsIm5iZiI6MTc0MTM0MTAwMywicGF0aCI6Ii8xNjQ2MDA5Mi80MjAyODM0ODUtMWYyODZmOTItOTk1Ny00YTkzLTg2MjQtODBiYWNlMjIxY2JhLnBuZz9YLUFtei1BbGdvcml0aG09QVdTNC1ITUFDLVNIQTI1NiZYLUFtei1DcmVkZW50aWFsPUFLSUFWQ09EWUxTQTUzUFFLNFpBJTJGMjAyNTAzMDclMkZ1cy1lYXN0LTElMkZzMyUyRmF3czRfcmVxdWVzdCZYLUFtei1EYXRlPTIwMjUwMzA3VDA5NTAwM1omWC1BbXotRXhwaXJlcz0zMDAmWC1BbXotU2lnbmF0dXJlPTk4NWI1M2EzNGYxMmYwYzFmNWU1MmZjMzRiNTY3ZjRlZDI5ZDhiZDI3YzE3ODdmMDJhMzQzNDVkYTQ1NTUzYzgmWC1BbXotU2lnbmVkSGVhZGVycz1ob3N0In0.IdXMTPk6IDsSIdhKxWeoRSZPg3KYcGFgmydEX1tbjq0)
![输入图片说明](https://private-user-images.githubusercontent.com/16460092/420283494-faef0fc5-b43e-4949-8c47-6242071835f8.png?jwt=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJnaXRodWIuY29tIiwiYXVkIjoicmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbSIsImtleSI6ImtleTUiLCJleHAiOjE3NDEzNDEzMDMsIm5iZiI6MTc0MTM0MTAwMywicGF0aCI6Ii8xNjQ2MDA5Mi80MjAyODM0OTQtZmFlZjBmYzUtYjQzZS00OTQ5LThjNDctNjI0MjA3MTgzNWY4LnBuZz9YLUFtei1BbGdvcml0aG09QVdTNC1ITUFDLVNIQTI1NiZYLUFtei1DcmVkZW50aWFsPUFLSUFWQ09EWUxTQTUzUFFLNFpBJTJGMjAyNTAzMDclMkZ1cy1lYXN0LTElMkZzMyUyRmF3czRfcmVxdWVzdCZYLUFtei1EYXRlPTIwMjUwMzA3VDA5NTAwM1omWC1BbXotRXhwaXJlcz0zMDAmWC1BbXotU2lnbmF0dXJlPWRlYzRjN2I5ODM4ODVmN2RjNGQyYWNjYmMyNzU1ZGI1MzUzNDJlZGQwM2VmNTU1ZjYzNWYxMTAyMTNhMWM2MGQmWC1BbXotU2lnbmVkSGVhZGVycz1ob3N0In0.gB8NQthuv2DoVIV17QaPamxOddxOkx9i5-2kN7w2L0M)
![输入图片说明](https://private-user-images.githubusercontent.com/16460092/420283467-0d73f19d-490e-4dd4-836b-87b18bbea1aa.png?jwt=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJnaXRodWIuY29tIiwiYXVkIjoicmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbSIsImtleSI6ImtleTUiLCJleHAiOjE3NDEzNDEzMDMsIm5iZiI6MTc0MTM0MTAwMywicGF0aCI6Ii8xNjQ2MDA5Mi80MjAyODM0NjctMGQ3M2YxOWQtNDkwZS00ZGQ0LTgzNmItODdiMThiYmVhMWFhLnBuZz9YLUFtei1BbGdvcml0aG09QVdTNC1ITUFDLVNIQTI1NiZYLUFtei1DcmVkZW50aWFsPUFLSUFWQ09EWUxTQTUzUFFLNFpBJTJGMjAyNTAzMDclMkZ1cy1lYXN0LTElMkZzMyUyRmF3czRfcmVxdWVzdCZYLUFtei1EYXRlPTIwMjUwMzA3VDA5NTAwM1omWC1BbXotRXhwaXJlcz0zMDAmWC1BbXotU2lnbmF0dXJlPWM2MzBkYWJlOGUwNmZmZDFjNGZiMzdlMTcxMWRiNTU1ZjNlYzc2YzdkYWE1MDE1ZGU5NmNlMzUzMzgxMTliM2QmWC1BbXotU2lnbmVkSGVhZGVycz1ob3N0In0.i0snxxTqp1TUNGcF62rA6nH9FTfhRCnuS5qF4U4VPz0)
