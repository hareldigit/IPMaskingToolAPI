using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace MaskingService.Utils
{
    public class IPAddressHelper
    {

        private static ILogger<IPAddressHelper> _logger = null;
        private Random m_random { get; set; }
        private readonly string m_ipPattern =@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b";

        public IPAddressHelper(ILogger<IPAddressHelper> logger)
        {
            _logger = logger;
            m_random = new Random();
        }

        public string GenerateIPNetworkAddress()
        {
            return $"{m_random.Next(1, 255)}.{m_random.Next(0, 255)}.{m_random.Next(0, 255)}";
        }

        public string GenerateIPComputerAddress()
        {
            return $"{m_random.Next(0, 255)}";
        }

        public IEnumerable<string> FindIPAddress(string text)
        {
            var ipRegex = new Regex(m_ipPattern);
            var matches = ipRegex.Matches(text);
            var result = matches.Cast<Match>()
            .Select(m => m.Value)
            .ToArray();
            return result;
        }

        public IPAddress ConvertToIPAddress(string ip)
        {
            var result = default(IPAddress);
            var ipRegex = new Regex(m_ipPattern);
            if (ipRegex.IsMatch(ip)) {
                result = IPAddress.Parse(ip);
            }
            else
            {
                _logger.LogError($"invalid ip address: {ip}");
            }
            return result;
        }
    }
}
