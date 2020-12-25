using MaskingService;
using MaskingService.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace MaskingTool.Test
{
    [TestClass]
    public class MaskManagerTest
    {
        [TestMethod]
        public void TestFindIPAddressMultiple()
        {
            var rows = new List<string>(){ 
                "03/22 08:51:06 INFO   :...read_physical_netif: index #0, interface VLINK1 has address 129.1.1.1, ifidx 0 " ,
                "03/22 08:51:06 INFO: ...read_physical_netif: index #1, interface TR1 has address 9.37.65.139, ifidx 1" };
            var maskManager = new MaskManager();
            var masked =  maskManager.Execute(rows);
            Assert.IsNotNull(masked);
            Assert.IsNotNull(masked.Lines);
            Assert.AreEqual(masked.Lines.Count(),2);
            Assert.IsNotNull(masked.Summery);
            Assert.AreEqual(masked.Summery.Count(), 2);
            Assert.AreEqual(masked.Summery.ElementAt(0).OriginalIP, "129.1.1.1");
            Assert.AreEqual(masked.Summery.ElementAt(1).OriginalIP, "9.37.65.139");
            var firstMasked = masked.Summery.ElementAt(0).MaskedIP;
            var secondMasked = masked.Summery.ElementAt(1).MaskedIP;
            Assert.AreNotEqual(firstMasked.NetworkAddress(), secondMasked.NetworkAddress());
            Assert.AreEqual(masked.Lines.ElementAt(0).IndexOf(firstMasked),86);
            Assert.AreEqual(masked.Lines.ElementAt(1).IndexOf(secondMasked),81);
        }

        [TestMethod]
        public void TestFindIPAddressMultipleWithSameClass()
        {
            var rows = new List<string>(){
                "03/22 08:51:06 INFO   :...read_physical_netif: index #0, interface VLINK1 has address 129.1.1.1, ifidx 0 " ,
                "03/22 08:51:06 INFO: ...read_physical_netif: index #1, interface TR1 has address 9.37.65.139, ifidx 1",
                "03/22 08:52:06 INFO: ...read_physical_netif: index #1, interface TR1 has address 9.37.65.138, ifidx 1" ,
                "03/22 08:52:06 INFO: ...read_physical_netif: index #1, interface TR1 has address 9.37.65.139, ifidx 1" };
            var maskManager = new MaskManager();
            var masked = maskManager.Execute(rows);
            Assert.IsNotNull(masked);
            Assert.IsNotNull(masked.Lines);
            Assert.AreEqual(masked.Lines.Count(), 4);
            Assert.IsNotNull(masked.Summery);
            Assert.AreEqual(masked.Summery.Count(), 3);
            Assert.AreEqual(masked.Summery.ElementAt(0).OriginalIP, "129.1.1.1");
            Assert.AreEqual(masked.Summery.ElementAt(1).OriginalIP, "9.37.65.139");
            Assert.AreEqual(masked.Summery.ElementAt(2).OriginalIP, "9.37.65.138");
            var firstMasked = masked.Summery.ElementAt(0).MaskedIP;
            var secondMasked = masked.Summery.ElementAt(1).MaskedIP;
            var thirdMasked = masked.Summery.ElementAt(2).MaskedIP;
            Assert.AreNotEqual(firstMasked.NetworkAddress(), secondMasked.NetworkAddress());
            Assert.AreEqual(secondMasked.NetworkAddress(), thirdMasked.NetworkAddress());
            Assert.AreNotEqual(secondMasked.ComputerAddress(), thirdMasked.ComputerAddress());
            Assert.AreEqual(masked.Lines.ElementAt(0).IndexOf(firstMasked), 86);
            Assert.AreEqual(masked.Lines.ElementAt(1).IndexOf(secondMasked), 81);
            Assert.AreEqual(masked.Lines.ElementAt(2).IndexOf(thirdMasked), 81);
            Assert.AreEqual(masked.Lines.ElementAt(3).IndexOf(secondMasked), 81);
        }
    }
}
