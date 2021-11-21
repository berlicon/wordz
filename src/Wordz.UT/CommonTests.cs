using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Wordz.Web.Helpers;

namespace Wordz.UT
{
    [TestFixture]
    public class CommonTests
    {
        [Test]
        public void TestReplaceScriptTags()
        {
            const string source = "<script>Hello World!</script>";
            var result = source.ReplaceScriptTags();
            Assert.AreEqual(false, result.Contains("script"));
        }
    }
}
