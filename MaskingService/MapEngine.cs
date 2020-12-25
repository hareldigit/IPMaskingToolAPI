using MaskingService.Utils;
using System.Collections.Generic;
using System.Linq;

namespace MaskingService
{
    public class MapEngine
    {
        private IEnumerable<string> _lines = null;
        public MapEngine(IEnumerable<string>  liens)
        {
            _lines = liens;
        }

        public Dictionary<string, Dictionary<string, HashSet<int>>> Execute()
        {
            var mappedIP = Map();
            return mappedIP;
        }

        private Dictionary<string, Dictionary<string, HashSet<int>>> Map()
        {
           var mappedIP = new Dictionary<string, Dictionary<string, HashSet<int>>>();
            for (int index = 0; index < _lines.Count(); index++)
            {
                var line = _lines.ElementAt(index);
                var ips = line.FindIPAddress().ToArray();
                if (ips.Length > 0)
                {
                    foreach (var ip in ips)
                    {
                        MapIP(ip, index, mappedIP);
                    }
                }
            }
            return mappedIP;
        }

        private void MapIP(string ip, int index, Dictionary<string, Dictionary<string, HashSet<int>>> mappedIP)
        {
            var mappedComputers = GetMappedComputers(ip, mappedIP);
            var computerLines = GetMappedComputerLines(ip, mappedComputers);
            computerLines.Add(index);
        }

        private Dictionary<string, HashSet<int>> GetMappedComputers(string ip, Dictionary<string, Dictionary<string, HashSet<int>>> mappedIP)
        {
            var network = ip.NetworkAddress();
            var networkComputers = mappedIP.SetKey(network);
            return networkComputers;
        }

        private HashSet<int> GetMappedComputerLines(string ip, Dictionary<string, HashSet<int>> mappedComputers)
        {
            var computer = ip.ComputerAddress();
            var computerLines = mappedComputers.SetKey(computer);
            return computerLines;
        }
    }
}
