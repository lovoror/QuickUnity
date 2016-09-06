using NUnit.Framework;
using QuickUnity.Localization;
using System.IO;
using UnityEngine;

namespace QuickUnity.Tests.UnitTests
{
    /// <summary>
    /// Unit Test cases for class LocalizationManager.
    /// </summary>
    [TestFixture]
    [Category("QuickUnity Tests/Unit Tests/LocalizationManager Test")]
    internal class LocalizationManagerTest
    {
        /// <summary>
        /// Test the function Initialize.
        /// </summary>
        [Test]
        public void InitializeTest()
        {
            string path = Path.Combine(Application.dataPath, "Localization");
            LocalizationManager.instance.Initialize(path);
            Assert.Pass();
        }
    }
}