using Garage_1._0.src;
using System.ComponentModel;

namespace Garage_1._0
{
    public static class Utils
    {
        public static string ReadString(string prompt, IUI ui)
        {
            string input;
            bool success = false;

            do
            {
                if(!string.IsNullOrWhiteSpace(prompt)) // Tom prompt skrivs ej ut
                    ui.WriteLine($"{prompt}");

                input = ui.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    ui.WriteLine($"Felaktig inmatning. Försök igen.");
                }
                else
                {
                    success = true;
                }
            }
            while (!success);

            return input;
        }

        public static uint ReadUnsignedInt(string prompt, IUI ui) //  Heltal inkl. 0
        {
            do
            {
                string input = ReadString(prompt, ui);

                if (uint.TryParse(input, out uint result))
                {
                    return result;
                }
                else
                {
                    ui.WriteLine($"Värdet måste vara ett heltal lika med eller större än 0. Försök igen.");
                }
            }
            while (true);
        }

        public static int ReadPositiveInt(string prompt, IUI ui)  // Heltal exkl. 0
        {
            do
            {
                string input = ReadString(prompt, ui);

                if (int.TryParse(input, out int result) && result > 0)
                {
                    return result;
                }
                else
                {
                    ui.WriteLine("Felaktigt värde. Försök igen");
                }
            }
            while (true);
        }

        public static VehicleType ReadVehicleTypeAsInt(string prompt, IUI ui)
        {
            do
            {                
                int input = ReadPositiveInt(prompt, ui);   // Om inmatad siffra >= 0 

                if (Enum.IsDefined(typeof(VehicleType), input))   // Om siffran ligger bakom en VehicleType
                {
                    return (VehicleType)input; 
                }
                else
                {                    
                    ui.WriteLine($"Felaktig fordonstyp. Försök igen. \n"); 
                }
            }
            while (true);

        }

        //public static FuelType ReadFuelTypeString(string prompt, IUI ui)
        //{
        //    do
        //    {
        //        uint input = ReadUnsignedInt(prompt, ui);   // Om man matat in en siffra >= 0 

        //        if (Enum.IsDefined(typeof(FuelType), (int)input))   // Om siffran ligger bakom en VehicleType
        //        {
        //            return (FuelType)input; 
        //        }
        //        else
        //        {
        //            //ToDo: Kolla om ReadUnsignedInt() ovan redan har returnerat ett felmeddelande
        //            ui.WriteLine($"Felaktig drivmedelstyp. Försök igen.");
        //        }
        //    }
        //    while (true);

        //}

        public static void WaitAndClear(IUI ui)
        {
            ui.WriteLine("\nTryck Enter för att återgå till huvudmenyn.");
            ui.ReadLine();
            ui.ClearScreen();
        }
 
        // Ger false vid all input förutom "J"
        public static bool ReadYes(string prompt, IUI ui)
        {
            ui.WriteLine(prompt + " (J = Ja, Enter = Nej):");
            string input = ui.ReadLine().Trim().ToUpper(); 
            return input == "J";
        }

        public static FuelType ReadFuelTypeAsInt(string prompt, IUI ui)
        {
            var values = Enum.GetValues<FuelType>();

            ui.WriteLine(prompt);

            foreach (FuelType f in values)
            {
                ui.WriteLine($"{(int)f}. {f.ToStringSWE()}");
            }

            int choice;

            while (true)
            {
                choice = ReadPositiveInt("", ui);        // Om inmatad siffra är 1 eller mer 

                if (Enum.IsDefined(typeof(FuelType), choice)) // Om siffran ligger bakom en VehicleType
                    return (FuelType)choice;

                ui.WriteLine("Felaktigt val. Försök igen.");
            }
        }

        public static decimal ReadDecimal(string prompt, IUI ui)
        {

            while (true)
            {
                ui.WriteLine(prompt);
                if (decimal.TryParse(ui.ReadLine(), out decimal value) && value >= 0)
                    return value;

                ui.WriteLine("Felaktigt värde. Försök igen.");
            }
        }
    }
}
