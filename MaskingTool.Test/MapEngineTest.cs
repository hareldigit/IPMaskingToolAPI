using MaskingService;
using MaskingService.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace MaskingTool.Test
{
    [TestClass]
    public class MapEngineTest
    {
        [TestMethod]
        public void TestMapEngineMultipleLines()
        {
            var lines = new List<string>(){ 
                "03/22 08:51:06 INFO   :...read_physical_netif: index #0, interface VLINK1 has address 129.1.1.1, ifidx 0 " ,
                "03/22 08:51:06 INFO: ...read_physical_netif: index #1, interface TR1 has address 9.37.65.139, ifidx 1" };
            var mapEngine = new MapEngine(lines);
            var mapped = mapEngine.Execute();
            Assert.IsNotNull(mapped);
            Assert.AreEqual(mapped.Count(),2);
            var firstNetwork = mapped.ElementAt(0).Key;
            var secondNetwork = mapped.ElementAt(1).Key;
            Assert.AreEqual(firstNetwork, "129.1.1");
            Assert.AreEqual(secondNetwork, "9.37.65");
            var firstNetworkComputers = mapped.ElementAt(0).Value;
            var secondNetworkComputers = mapped.ElementAt(1).Value;
            Assert.AreEqual(firstNetworkComputers.Count(), 1);
            Assert.AreEqual(secondNetworkComputers.Count(), 1);
            var firstNetworkComputer = firstNetworkComputers.ElementAt(0).Key;
            Assert.AreEqual(firstNetworkComputer, "1");
            var secondNetworkComputer = secondNetworkComputers.ElementAt(0).Key;
            Assert.AreEqual(secondNetworkComputer, "139");
            var firstNetworkComputerLines = firstNetworkComputers.ElementAt(0).Value;
            Assert.AreEqual(firstNetworkComputerLines.Count(), 1);
            Assert.AreEqual(firstNetworkComputerLines.ElementAt(0), 0);
            var secondNetworkComputerLines = secondNetworkComputers.ElementAt(0).Value;
            Assert.AreEqual(secondNetworkComputerLines.Count(), 1);
            Assert.AreEqual(secondNetworkComputerLines.ElementAt(0), 1);
        }

        [TestMethod]
        public void TestMapEngineMultipleLinesWithSameClass()
        {
            var lines = new List<string>(){
                "03/22 08:51:06 INFO   :...read_physical_netif: index #0, interface VLINK1 has address 129.1.1.1, ifidx 0 " ,
                "03/22 08:51:06 INFO: ...read_physical_netif: index #1, interface TR1 has address 9.37.65.139, ifidx 1",
                "03/22 08:52:06 INFO: ...read_physical_netif: index #1, interface TR1 has address 9.37.65.138, ifidx 1" ,
                "03/22 08:52:06 INFO: ...read_physical_netif: index #1, interface TR1 has address 9.37.65.139, ifidx 1" };
            var mapEngine = new MapEngine(lines);
            var mapped = mapEngine.Execute();
            Assert.IsNotNull(mapped);
            Assert.AreEqual(mapped.Count(), 2);
            var firstNetwork = mapped.ElementAt(0).Key;
            var secondNetwork = mapped.ElementAt(1).Key;
            Assert.AreEqual(firstNetwork, "129.1.1");
            Assert.AreEqual(secondNetwork, "9.37.65");
            var firstNetworkComputers = mapped.ElementAt(0).Value;
            var secondNetworkComputers = mapped.ElementAt(1).Value;
            Assert.AreEqual(firstNetworkComputers.Count(), 1);
            Assert.AreEqual(secondNetworkComputers.Count(), 2);
            var firstNetworkComputer = firstNetworkComputers.ElementAt(0).Key;
            Assert.AreEqual(firstNetworkComputer, "1");
            var secondNetworkComputer139 = secondNetworkComputers.ElementAt(0).Key;
            Assert.AreEqual(secondNetworkComputer139, "139");
            var secondNetworkComputer138 = secondNetworkComputers.ElementAt(1).Key;
            Assert.AreEqual(secondNetworkComputer138, "138");
            var firstNetworkComputerLines = firstNetworkComputers.ElementAt(0).Value;
            Assert.AreEqual(firstNetworkComputerLines.Count(), 1);
            Assert.AreEqual(firstNetworkComputerLines.ElementAt(0), 0);
            var secondNetworkComputer139Lines = secondNetworkComputers.ElementAt(0).Value;
            Assert.AreEqual(secondNetworkComputer139Lines.Count(), 2);
            Assert.AreEqual(secondNetworkComputer139Lines.ElementAt(0), 1);
            Assert.AreEqual(secondNetworkComputer139Lines.ElementAt(1), 3);
            var secondNetworkComputer138Lines = secondNetworkComputers.ElementAt(1).Value;
            Assert.AreEqual(secondNetworkComputer138Lines.Count(), 1);
            Assert.AreEqual(secondNetworkComputer138Lines.ElementAt(0), 2);
        }
    }
}
