using NUnit.Framework;
using QuickUnity.Config;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace QuickUnity.UnitTests
{
    /// <summary>
    /// Unit test cases for class IniConfigFile.
    /// </summary>
    [TestFixture]
    [Category("IniConfigFileTests")]
    internal class IniConfigFileTests
    {
        /// <summary>
        /// Creates the ini configuration file test.
        /// </summary>
        [Test]
        public void CreateIniConfigFileTest()
        {
            IniConfigFile configFile = new IniConfigFile();
            configFile.AddOrUpdateValue("Test", "Hello", "你好");
            configFile.AddOrUpdateValue("Test", "IsMale", true);
            configFile.AddOrUpdateValue("Test", "FloatValue", 12.993219321);
            configFile.AddOrUpdateValue("Test", "IntValue", 123);

            configFile.AddOrUpdateListValue("Test2", "StringList", new List<string>() { "Hello!", "Thanks!" });
            configFile.AddOrUpdateListValue("Test2", "BoolList", new List<bool>() { true, false });
            configFile.AddOrUpdateListValue("Test2", "FloatList", new List<float>() { 11.111f, 22.222f, 333.333f });
            configFile.AddOrUpdateListValue("Test2", "IntList", new List<int>() { 1111, 2222, 3333 });

            string path = Path.Combine(Application.persistentDataPath, "Config");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string iniFilePath = Path.Combine(path, "test.ini");
            configFile.Save(iniFilePath);

            IniConfigFile verifiedConfigFile = IniConfigFile.Create(iniFilePath);

            Assert.AreEqual(verifiedConfigFile.GetValue("Test", "Hello"), "你好");
            Assert.AreEqual(verifiedConfigFile.GetValue<bool>("Test", "IsMale"), true);
            Assert.AreEqual(verifiedConfigFile.GetValue<double>("Test", "FloatValue"), 12.993219321, double.Epsilon);
            Assert.AreEqual(verifiedConfigFile.GetValue<int>("Test", "IntValue"), 123);

            Assert.AreEqual(verifiedConfigFile.GetListValue<string>("Test2", "StringList"), new List<string>() { "Hello!", "Thanks!" });
            Assert.AreEqual(verifiedConfigFile.GetListValue<bool>("Test2", "BoolList"), new List<bool>() { true, false });
            Assert.AreEqual(verifiedConfigFile.GetListValue<float>("Test2", "FloatList"), new List<float>() { 11.111f, 22.222f, 333.333f });
            Assert.AreEqual(verifiedConfigFile.GetListValue<int>("Test2", "IntList"), new List<int>() { 1111, 2222, 3333 });
        }

        /// <summary>
        /// Parses the ini configuration file test.
        /// </summary>
        [Test]
        public void ParseIniConfigFileTest()
        {
            string path = Path.Combine(Application.persistentDataPath, "Config");
            string iniFilePath = Path.Combine(path, "test.ini");

            IniConfigFile testFile = IniConfigFile.Create(iniFilePath);
            Assert.AreEqual(testFile.GetValue("Test", "Hello"), "你好");
            Assert.AreEqual(testFile.GetValue<bool>("Test", "IsMale"), true);
            Assert.AreEqual(testFile.GetValue<double>("Test", "FloatValue"), 12.993219321, double.Epsilon);

            Assert.AreEqual(testFile.GetListValue<string>("Test2", "StringList"), new List<string>() { "Hello!", "Thanks!" });
            Assert.AreEqual(testFile.GetListValue<bool>("Test2", "BoolList"), new List<bool>() { true, false });
            Assert.AreEqual(testFile.GetListValue<float>("Test2", "FloatList"), new List<float>() { 11.111f, 22.222f, 333.333f });
            Assert.AreEqual(testFile.GetListValue<int>("Test2", "IntList"), new List<int>() { 1111, 2222, 3333 });
        }
    }
}