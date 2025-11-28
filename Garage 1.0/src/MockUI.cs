using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Garage_1._0.src
{
    internal class MockUI : IUI
    {
        public MockUI() { }

        // Todo: GetInput() {Return SetInput;} , SetInput {get; set;} = "[värde]", Print(), PrintLine()(?)

        public string SetInput { get; set; } = "1";  // Kan sättas till valfritt värde inifrån testet (tex expected), motsvarar readline() i metoder

        public string SetOutput { get; set; } = "";  // Kan hämtas inifrån testet (tex expected), motsvarar writeline() i metoder

        public void ClearScreen()
        {
            
        }

        public int Read()
        {
            throw new NotImplementedException();
        }

        public string ReadLine()
        {
            return SetInput;
        }

        public void Write(string text = "")
        {
            SetOutput = text;
        }

        public void WriteLine(string text = "")
        {
            SetOutput = text;
        }

        
    }
}
