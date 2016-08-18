using NUnit.Framework;
using QuickUnity.Config;
using System.Collections.Generic;
using System.IO;
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

        /// <summary>
        /// Generates the ini configuration file test.
        /// </summary>
        [Test]
        public void GenerateINIConfigFileTest()
        {
            INIConfigFile configFile = new INIConfigFile("test");
            configFile.AddOrUpdateValue("Test", "Hello", "你好");
            configFile.AddOrUpdateValue("Test", "IsMale", true);
            configFile.AddOrUpdateValue("Test", "FloatValue", 12.993219321);
            configFile.AddOrUpdateValue("Test", "IntValue", 123);

            configFile.AddOrUpdateListValue("Test2", "BoolList", new List<bool>() { true, false });
            configFile.AddOrUpdateListValue("Test2", "FloatList", new List<float>() { 11.111f, 22.222f, 333.333f });
            configFile.AddOrUpdateListValue("Test2", "IntList", new List<int>() { 1111, 2222, 3333 });

            string path = Path.Combine(Application.persistentDataPath, "Config");
            configFile.Save(path);

            string iniFilePath = Path.Combine(path, "test.ini");
            INIConfigFile verifiedConfigFile = INIConfigFile.ParseINIConfigFile(iniFilePath);

            Assert.AreEqual(verifiedConfigFile.GetValue("Test", "Hello"), "你好");
            Assert.AreEqual(verifiedConfigFile.GetValue<bool>("Test", "IsMale"), true);
            Assert.AreEqual(verifiedConfigFile.GetValue<double>("Test", "FloatValue"), 12.993219321, double.Epsilon);
            Assert.AreEqual(verifiedConfigFile.GetValue<int>("Test", "IntValue"), 123);

            Assert.AreEqual(verifiedConfigFile.GetListValue<bool>("Test2", "BoolList"), new List<bool>() { true, false });
            Assert.AreEqual(verifiedConfigFile.GetListValue<float>("Test2", "FloatList"), new List<float>() { 11.111f, 22.222f, 333.333f });
            Assert.AreEqual(verifiedConfigFile.GetListValue<int>("Test2", "IntList"), new List<int>() { 1111, 2222, 3333 });
        }
    }
}