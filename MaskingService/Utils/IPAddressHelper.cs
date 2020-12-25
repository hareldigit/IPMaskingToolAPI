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
        private Random m_random { get; set; }

        public IPAddressHelper()
        {
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
    }
}
