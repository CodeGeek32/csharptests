using APITests.Util;
using NUnit.Framework;
using System.Net.Http;
using System.Net.Http.Headers;

namespace APITests
{
    public class TestMethodOrdering
    {
        //[SetUp]
        [OneTimeSetUp]
        public void SetUp()
        {
            System.Diagnostics.Trace.WriteLine("setup method invoked");
        }

        [Test, Order(2)]
        public void TestMethod1()
        {
            System.Diagnostics.Trace.WriteLine("System.Diagnostics.Trace.WriteLine.TestMethod1");
            //TestContext.Out.WriteLine("TestMethod1");
            Assert.AreEqual(1, 1);
        }

        [Test, Order(1)]
        public void Test2()
        {
            System.Diagnostics.Trace.WriteLine("System.Diagnostics.Trace.WriteLine.Test2");
            //TestContext.Out.WriteLine("Test2");
            Assert.AreEqual(1, 1);
        }
    }
}
