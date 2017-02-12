using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MonoGame.ContentPipeline.Extensions.Blender.Tests
{
    [TestClass]
    public class UnitTest1
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestConvert()
        {
            var input = "Cube.blend";
            var output = Path.Combine(TestContext.TestResultsDirectory, Path.ChangeExtension(input, ".fbx"));

            BlenderAdapter.ToFbx(input, output);
        }
    }
}
