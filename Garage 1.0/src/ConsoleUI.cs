using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_1._0.src
{
    // Klassen kapslar in Console-funktioner, för att hålla resten av programmet oberoende av Console.
    // Syftet är att kunna byta ut UI-lagret utan att ändra logik i programmets hantering av textinput och -output, rensa skärm osv.
    // Genom att låta ConsoleUI implementera ett IUI-interface kan huvudprogrammet arbeta mot detta interface istället för konkreta Console-anrop.
    // Detta ger mindre dependency, bättre testbarhet och tydligare fördelning av ansvar i applikationen.
    internal class ConsoleUI : IUI
    {
        public ConsoleUI() { }  // Todo: ska ConsoleUI ta emot Log som parameter? 

        public void WriteLine(string text="")
        {
            Console.WriteLine(text);
        }

        public void Write(string text="")
        {
            Console.Write(text);
        }

        public string ReadLine()  
        {
            return Console.ReadLine() ?? string.Empty;
        }

        public int Read() // Todo: Tveksam. Man bör inte exponera ett lågnivå-Console-API i ett abstraktionslager. Behåll endast ReadLine().
        {
            return Console.Read()!;
        }

        public void ClearScreen()  // Todo:Detta gör kanske UI svårare att testa?
        {
            Console.Clear();
        }

        //Todo: Ska man returnera ConsoleKey osv från dessa?
        // Todo: När man använder tex Write och WriteLine ute i koden, ska man då lägga dem i en variabler innan de används, tex var input = ConsoleUI.ReadLine() osv?

    }
}
