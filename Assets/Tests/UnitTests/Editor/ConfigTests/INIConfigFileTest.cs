using NUnit.Framework;
using QuickUnity.Config;

namespace QuickUnity.Tests.UnitTests.ConfigTests
{
    /// <summary>
    /// Test cases for class INIConfigFile.
    /// </summary>
    internal class INIConfigFileTest
    {
        /// <summary>
        /// Parses the INI configuration file test.
        /// </summary>
        [Test]
        public void ParseINIConfigFileTest()
        {
            INIConfigFile configFile = INIConfigFile.ParseINIConfigFile("Config/system");
            Assert.IsNotNull(configFile);
        }
    }
}