using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GUANJI
{
    public static class NetworkHelper
    {
        /// <summary>
        /// 检查本机是否有网络连接，并获取到有效的IPv4地址
        /// </summary>
        /// <returns>如果有网络且能获取到IP地址，则返回true，否则返回false</returns>
        public static bool IsNetworkAvailable(out IPAddress localIp)
        {
            localIp = null;
            // 首先判断网络连接状态
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                return false;
            }
            try
            {
                // 获取本机所有IPv4地址
                var host = Dns.GetHostEntry(Dns.GetHostName());
                localIp = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && !IPAddress.IsLoopback(ip));
                return localIp != null;
            }
            catch
            {
                return false;
            }
        }
    }
}
