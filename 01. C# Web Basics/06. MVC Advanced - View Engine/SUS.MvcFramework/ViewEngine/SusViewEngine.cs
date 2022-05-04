using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SUS.MvcFramework.ViewEngine
{
    public class SusViewEngine : IViewEngine
    {
        public string GetHtml(string templateCode, object viewModel)
        {
            string charpCode = GenerateCSharpFromTemplate(templateCode);
            IView executableObject = GenerateExecutableCode(charpCode, viewModel);
            string html = executableObject.ExecuteTemplate(viewModel);
            return html;
        }

        private string GenerateCSharpFromTemplate(string templateCode)
        {
            string methodBody = GetMethodBody(templateCode);
            string csharpCode = @"
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using SUS.MvcFramework.ViewEngine;

namespace ViewNamespace
{
    public class ViewClass : IView
    {
        public string ExecuteTemplate(object viewModel)
        {
            var html = new StringBuilder();
            " + methodBody + @"
            return html.ToString();
        }
    }
}
";


            return csharpCode;
        }

        private string GetMethodBody(string templateCode)
        {
            return string.Empty;
        }

        private IView GenerateExecutableCode(string charpCode, object viewModel)
        {
            var compileResult = CSharpCompilation.Create("ViewAssembly")
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                .AddReferences(MetadataReference.CreateFromFile(typeof(IView).Assembly.Location));

            if (viewModel != null)
            {
                compileResult = compileResult
                    .AddReferences(MetadataReference.CreateFromFile(viewModel.GetType().Assembly.Location));
            }

            var libraries = Assembly.Load(new AssemblyName("netstandard"))
                .GetReferencedAssemblies();

            foreach (var library in libraries)
            {
                compileResult = compileResult
                    .AddReferences(MetadataReference.CreateFromFile(
                        Assembly.Load(library).Location));
            }

            compileResult = compileResult
                .AddSyntaxTrees(SyntaxFactory.ParseSyntaxTree(charpCode));

            using MemoryStream memoryStream = new MemoryStream();

            EmitResult result = compileResult.Emit(memoryStream);
            if (!result.Success)
            {
                // Compile errors!
                return new ErrorView(result.Diagnostics
                    .Where(x => x.Severity == DiagnosticSeverity.Error)
                    .Select(x => x.GetMessage()), charpCode);
            }

            memoryStream.Seek(0, SeekOrigin.Begin);
            var byteAssembly = memoryStream.ToArray();
            var assembly = Assembly.Load(byteAssembly);
            var viewType = assembly.GetType("ViewNamespace.ViewClass");
            var instance = (IView)Activator.CreateInstance(viewType);

            return instance;
        }
    }
}
