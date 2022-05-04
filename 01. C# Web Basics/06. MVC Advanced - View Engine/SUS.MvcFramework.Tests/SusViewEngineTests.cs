using SUS.MvcFramework.ViewEngine;
using System;
using System.IO;
using Xunit;

namespace SUS.MvcFramework.Tests
{
    public class SusViewEngineTests
    {
        [Theory]
        // happy path
        // interesting cases
        // complex cases or combination of tests
        // code coverage 100%
        [InlineData("CleanHtml")]
        [InlineData("Foreach")]
        [InlineData("IfElseFor")]
        [InlineData("ViewModel")]
        public void TestGetHtml(string fileName)
        {
            var viewModel = new TestViewModel
            {
                DateOfBirth = new DateTime(2019, 6, 1),
                Name = "Doggo",
                Rrice = 1234.24M,
            };

            IViewEngine viewEngine = new SusViewEngine();

            var view = File.ReadAllText($"ViewTests/{fileName}.html");
            var result = viewEngine.GetHtml(view, viewModel);
            var expectedResult = File.ReadAllText($"ViewTests/{fileName}.Result.html");

            Assert.Equal(expectedResult, result);
        }

        public class TestViewModel
        {
            public string Name { get; set; }

            public decimal Rrice { get; set; }

            public DateTime DateOfBirth { get; set; }
        }
    }
}
