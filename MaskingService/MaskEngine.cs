using MaskingService.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MaskingService
{
    public class MaskEngine
    {
        private IEnumerable<string> _lines = null;
        private Dictionary<string, Dictionary<string, HashSet<int>>> _mappedIP = null;
        private List<IPSubmission> _ipSubmissions = null;

        public MaskEngine(IEnumerable<string> lines, Dictionary<string, Dictionary<string, HashSet<int>>> mappedIP)
        {
            _lines = lines;
            _mappedIP = mappedIP;
            _ipSubmissions = new List<IPSubmission>();
        }

        public MaskResult Execute()
        {
            var maskedLines = MaskLines();
            var result = new MaskResult()
            {
                Lines = maskedLines,
                Summery = _ipSubmissions

            };
            return result;
        }

        private IEnumerable<string> MaskLines()
        {
            var maskedLines = _lines.ToArray();
            var ipAddressHelper = new IPAddressHelper();
            var usedMaskedNetwork = new List<string>();
            foreach (var network in _mappedIP.Keys)
            {
                maskedLines = MaskNetworkComputers(network, maskedLines, ipAddressHelper, usedMaskedNetwork);
            }
            return maskedLines;
        }

        private string[] MaskNetworkComputers(string network, string[] result, IPAddressHelper ipAddressHelper, List<string> usedMaskedNetwork)
        {
            var computers = _mappedIP[network];
            string maskedNetwork = GenerateUniqueIP(usedMaskedNetwork, ipAddressHelper.GenerateIPNetworkAddress);
            foreach (var computer in computers.Keys)
            {
                var usedMaskedComputer = new List<string>();
                var origIP = BuildIP(network, computer);
                var relevantLines = computers[computer];
                var maskedIP = MaskComputer(ipAddressHelper, maskedNetwork, usedMaskedComputer);
                AddSubmission(origIP, maskedIP);
                result = MaskLines(relevantLines, origIP, maskedIP, result);
            }

            return result;
        }
        private string MaskComputer(IPAddressHelper ipAddressHelper, string maskedNetwork, List<string> usedMaskedComputer)
        {
            var maskedComputer = GenerateUniqueIP(usedMaskedComputer, ipAddressHelper.GenerateIPComputerAddress);
            var maskedIP = BuildIP(maskedNetwork, maskedComputer);
            return maskedIP;
        }

        private string BuildIP(string network, string computer)
        {

            var result = $"{network}.{computer}";
            return result;
        }

        private void AddSubmission(string origIP, string maskedIP)
        {
            var ipSubmission = new IPSubmission(origIP, maskedIP);
            _ipSubmissions.Add(ipSubmission);
        }


        private string[] MaskLines(HashSet<int> lines, string origIP, string maskedIP, string[] result)
        {
            foreach (var index in lines)
            {
                var text = result[index];
                var maskedText = Regex.Replace(text, origIP, maskedIP);
                result[index] = maskedText;
            }
            return result;
        }

        private string GenerateUniqueIP(List<string> usedMasked, Func<string> generateIPAddress)
        {
            var masked = generateIPAddress();
            while (usedMasked.Contains(masked))
            {
                masked = generateIPAddress();
            }
            usedMasked.Add(masked);
            return masked;
        }
    }
}
