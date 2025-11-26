using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_1._0.src
{
    internal class MockUI : IUI
    {
        public MockUI() { }

        // Todo: GetInput() {Return SetInput;} , SetInput {get; set;} = "[värde]", Print(), PrintLine()(?)

        public string SetInput { get; set; } = "1";  // Kan sättas till valfritt värde inifrån testet (tex expected), motsvarar readline() i metoder

        public void ClearScreen()
        {
            throw new NotImplementedException();
        }

        public int Read()
        {
            throw new NotImplementedException();
        }

        public string ReadLine()
        {
            throw new NotImplementedException();
        }

        public void Write(string text = "")
        {
            throw new NotImplementedException();
        }

        public void WriteLine(string text = "")
        {
            throw new NotImplementedException();
        }

        
    }
}
