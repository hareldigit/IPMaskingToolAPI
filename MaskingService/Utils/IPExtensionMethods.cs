using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MaskingService.Utils
{
    public static class IPExtensionMethods
    {
        private static string m_ipPattern = @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b";
        private static string GetNetworkAddress(this IPAddress ipAddress)
        {
            var networkAddress = default(string);
            networkAddress = ipAddress.ToString().Substring(0, ipAddress.ToString().LastIndexOf('.'));
            return networkAddress;
        }

        private static string GetComputerAddress(this IPAddress ipAddress)
        {
            var computerAddress = default(string);
            computerAddress = ipAddress.ToString().Substring(ipAddress.ToString().LastIndexOf('.') + 1);
            return computerAddress;
        }

        public static string NetworkAddress(this string ip)
        {
            var networkAddress = default(string);
            var ipAddress = ip.AsIPAddress();
            networkAddress= ipAddress.GetNetworkAddress();
            return networkAddress;
        }

        public static string ComputerAddress(this string ip)
        {
            var computerAddress = default(string);
            var ipAddress = ip.AsIPAddress();
            computerAddress = ipAddress.GetComputerAddress();
            return computerAddress;
        }

        public static IPAddress AsIPAddress(this string ip)
        {
            var result = default(IPAddress);
            var ipRegex = new Regex(m_ipPattern);
            if (ipRegex.IsMatch(ip))
            {
                result = IPAddress.Parse(ip);
            }
            else
            {
                throw (new FormatException($"invalid ip address: {ip}"));
            }
            return result;
        }

        public static IEnumerable<string> FindIPAddress(this string text)
        {
            var ipRegex = new Regex(m_ipPattern);
            var matches = ipRegex.Matches(text);
            var result = matches.Cast<Match>()
            .Select(m => m.Value)
            .ToArray();
            return result;
        }
    }
}
