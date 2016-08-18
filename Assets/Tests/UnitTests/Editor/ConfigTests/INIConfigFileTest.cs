using NUnit.Framework;
using QuickUnity.Config;
using System.Collections.Generic;
using UnityEngine;

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
            Assert.AreEqual(configFile.GetValue("TestSection1", "test1"), "test1");
            Assert.AreEqual(configFile.GetValue<int>("TestSection1", "test2"), 16);
            Assert.AreEqual(configFile.GetValue<double>("TestSection1", "test3"), 3214.321431, double.Epsilon);
            Assert.AreEqual(configFile.GetValue<bool>("TestSection1", "test4"), true);

            List<string> strList = configFile.GetListValue("TestSection2", "test5");
            Assert.IsNotNull(strList);

            List<int> intList = configFile.GetListValue<int>("TestSection2", "test6");
            Assert.IsNotNull(intList);

            List<float> floatList = configFile.GetListValue<float>("TestSection2", "test7");
            Assert.IsNotNull(floatList);

            List<bool> boolList = configFile.GetListValue<bool>("TestSection2", "test8");
            Assert.IsNotNull(boolList);
        }
    }
}