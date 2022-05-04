using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS.MvcFramework.ViewEngine
{
    public class SusViewEngine : IViewEngine
    {
        public string GetHtml(string templateCode, object viewModel)
        {
            string charpCode = GenerateCSharpFromTemplate(templateCode);
            IView executableObject = GenerateExecutableCode(charpCode);
            string html = executableObject.ExecuteTemplate(viewModel);
            return html;
        }

        private string GenerateCSharpFromTemplate(string templateCode)
        {
            string csharpCode = "";



            return csharpCode;
        }

        private IView GenerateExecutableCode(string charpCode)
        {
            throw new NotImplementedException();
        }
    }
}
