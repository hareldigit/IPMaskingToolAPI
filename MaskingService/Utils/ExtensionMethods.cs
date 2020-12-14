using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MaskingService.Utils
{
    public static class ExtensionMethods
    {
        public static string GetNetworkAddress(this IPAddress ipAddress)
        {
            var networkAddress = default(string);
            networkAddress = ipAddress.ToString().Substring(0, ipAddress.ToString().LastIndexOf('.'));
            return networkAddress;
        }

        public static string GetComputerAddress(this IPAddress ipAddress)
        {
            var computerAddress = default(string);
            computerAddress = ipAddress.ToString().Substring(ipAddress.ToString().LastIndexOf('.') + 1);
            return computerAddress;
        }
    }
}
