using NUnit.Framework;
using QuickUnity.Config;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace QuickUnity.Config
{
    /// <summary>
    /// Unit test cases for class YamlSettingsFile.
    /// </summary>
    [TestFixture]
    internal class YamlSettingsFileTests
    {
        /// <summary>
        /// Class TestSettings to test YAML format serialization and deserialization.
        /// </summary>
        internal class TestSettings
        {
            public int intValue
            {
                get;
                set;
            }

            public float floatValue
            {
                get;
                set;
            }

            public string stringValue
            {
                get;
                set;
            }

            public List<bool> boolListValue
            {
                get;
                set;
            }

            public Dictionary<string, bool> boolDictValue
            {
                get;
                set;
            }
        }

        /// <summary>
        /// Test case for method Serialize
        /// </summary>
        [Test]
        public void SerializeTest()
        {
            TestSettings settings = new TestSettings();
            settings.intValue = int.MaxValue;
            settings.floatValue = float.MaxValue;
            settings.stringValue = "test你好！";
            settings.boolListValue = new List<bool>()
            {
                true, false, true, false
            };
            settings.boolDictValue = new Dictionary<string, bool>()
            {
                { "test1", true },
                { "测试2", false }
            };

            YamlSettingsFile.Serialize(Application.persistentDataPath, settings);
            string filePath = Path.Combine(Application.persistentDataPath, "TestSettings.asset");
            Assert.IsTrue(File.Exists(filePath));
        }

        /// <summary>
        /// Test case for method Deserialize
        /// </summary>
        [Test]
        public void DeserializeTest()
        {
            TestSettings settings = YamlSettingsFile.Deserialize<TestSettings>(Application.persistentDataPath);

            if (settings != null)
            {
                Assert.AreEqual(settings.intValue, int.MaxValue);
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}