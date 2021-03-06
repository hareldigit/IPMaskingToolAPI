using MaskingService.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text.RegularExpressions;

namespace MaskingTool.Test
{
    [TestClass]
    public class IPAddressHelperTest
    {
        [TestMethod]
        public void TestGenerateIPNetworkAddress()
        {
            var ipAddressHelper = new IPAddressHelper();
            var ipNetworkAddress = ipAddressHelper.GenerateIPNetworkAddress();
            var ipRegex = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\b");
            var isMatch = ipRegex.IsMatch(ipNetworkAddress);
            Assert.IsTrue(isMatch);
        }

        [TestMethod]
        public void TestGenerateIPComputerAddress()
        {
            var ipAddressHelper = new IPAddressHelper();
            var ipIPComputerAddress = ipAddressHelper.GenerateIPComputerAddress();
            var ipRegex = new Regex(@"\b\d{1,3}");
            var isMatch = ipRegex.IsMatch(ipIPComputerAddress);
            Assert.IsTrue(isMatch);
        }

        [TestMethod]
        public void TestFindIPAddressSingle()
        {
            var text = "03/22 08:51:06 INFO   :...read_physical_netif: index #0, interface VLINK1 has address 129.1.1.1, ifidx 0";
            var ipAddressHelper = new IPAddressHelper();
            var ips = text.FindIPAddress().ToArray();
            Assert.IsNotNull(ips);
            Assert.AreEqual(ips.Length,1);
            Assert.AreEqual(ips[0], "129.1.1.1");
        }

        [TestMethod]
        public void TestFindIPAddressMultiple()
        {
            var text = "03/22 08:51:06 INFO   :...read_physical_netif: index #0, interface VLINK1 has address 129.1.1.1, ifidx 0 " +
                       "03/22 08:51:06 INFO: ...read_physical_netif: index #1, interface TR1 has address 9.37.65.139, ifidx 1";
            var ipAddressHelper = new IPAddressHelper();
            var ips = text.FindIPAddress().ToArray();
            Assert.IsNotNull(ips);
            Assert.AreEqual(ips.Length, 2);
            Assert.AreEqual(ips[0], "129.1.1.1");
            Assert.AreEqual(ips[1], "9.37.65.139");
        }

        [TestMethod]
        public void TestGetNetworkAddress()
        {
            var text = "9.37.65.139";
            Assert.IsNotNull(text);
            var networkAddress = text.NetworkAddress();
            Assert.AreEqual(networkAddress, "9.37.65");;
        }

        [TestMethod]
        public void TestGetComputerAddress()
        {
            var text = "9.37.65.139";
            Assert.IsNotNull(text);
            var computerAddress = text.ComputerAddress();
            Assert.AreEqual(computerAddress, "139");
        }
    }
}
