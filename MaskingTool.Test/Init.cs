using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaskingTool.Test
{
    [TestClass()]
    public class Init
    {
        [AssemblyInitialize]
        public static void initialize(TestContext testContext)
        {
            ConfigureNLog();
        }

        private static void ConfigureNLog()
        {
            var configFilePath = "NLog.Config";
            var services = new ServiceCollection()
                .AddLogging(ConfigureNLog => ConfigureNLog.AddNLog(configFilePath));
        }
    }
}
