using Garage_1._0.src.Factories;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Garage_1._0.src
{
    /*
     * GarageHandler abstraherar ut ett lager mellan GUI:t och klassen Garage, så att Garage inte är beroende av något GUI. 
     * Klassen hanterar funktionaliteten som gränssnittet behöver ha tillgång till.
     */

    // Todo: IHandler ska läggas till som superklass.
    // Behöver inte vara generisk, men denna klass ska i förlängningen kunna hålla olika garage - Covariance (olika typer av garage ska kunna ligga i en lista)
    // Todo: Även GarageManager ska läggas till, vad ska då ligga i denna klass och i GarageManagern?

    internal class GarageHandler
    {
        // Todo: Skapa jsonfil "appsettings.json" med nycklar för ui + ev. garagestorlek, etc.  (garage {ui = console} }
        // Add -> Json Configuration file. När den är klar: Högerklick -> Properties -> 'Copy if newer' lägger till den i 
        // Lägg till using Microsoft.Extensions.Configuration
        // Sedan läs ut värden med:
        // IConfiguration config = new ConfigurationBuilder() // Builder pattern för logging. Finns i nuget: Microsoft .Extensions.Configuration.FileProviders, .Json, 
        // .SetBasePath(Environment.CurrentDirectory.AddJsonFile("appsettings.json", optional:false, reloadOnChange: true).Build()
        // var ui = config.GetSection("garage:ui").Value;
        // if ui = console { IUI = new ConsoleUI ...}

        private IUI _ui;                        // Todo: Vill man kunna sätta om iui här på annat sätt än genom konstruktorn?

        protected Garage<Vehicle> _garage = null!;        // Todo: Denna bör initieras, antingen i konstruktorn eller hårdkodat (annars får man varningar överallt). Hur ska man tänka här?

        public GarageHandler(IUI ui)            // Todo: Kasta ArgumentException.ThrowIfNullOrWhitespace osv för att skydda mot fel värden i konstruktorer. Även om utility-klasser kollar inmatningar.
        {
            ArgumentNullException.ThrowIfNull(ui);
            _ui = ui;
            //_garage = new Garage<Vehicle>(capacity: 10); // Todo: Ska denna instans skapas här? Byt ut hårdkodat argument Capacity mot  defaultvärde ur en config-fil då kanske? 
        }

        internal void Run()
        {
            CreateGarageMenu();
            SeedGarage();
            CreateMainMenu();
        }

        //private void CreateGarage()
        //{
        //    // Todo: Meny enligt följande

        //    // Välkommen till Garage 1.0!
        //    //
        //    // Var god välj något av följande:
        //    //
        //    // 1. Skapa ett standardgarage med 20 platser och 10 parkerade fordon.
        //    // 2. Skapa ett eget tomt garage med valfritt antal parkeringsplatser.
        //    // 0. Avsluta

        //    int input = Utils.ReadPositiveInt("Ange antalet platser i garaget:", _ui);
        //    _garage = new Garage<Vehicle>(capacity: input);

        //}

        private void CreateGarageMenu()
        {
            bool success = true;

            do
            {
                _ui.ClearScreen();

                if (!success)
                    _ui.WriteLine("Felaktigt val. Försök igen.\n");

                string menuText = "Välkommen till Garage 1.0! \n \nVar god välj:\n1. Skapa standardgarage (20 platser, 10 fordon)\n2. Skapa eget tomt garage\n0. Avsluta";

                uint choice = Utils.ReadUnsignedInt(menuText, _ui);

                switch (choice)
                {
                    case 1:
                        _garage = new Garage<Vehicle>(20);
                        SeedGarage();
                        return;

                    case 2:
                        int size = Utils.ReadPositiveInt("Ange antal parkeringsplatser:", _ui);
                        _garage = new Garage<Vehicle>(size);
                        return;

                    case 0:
                        Environment.Exit(0);
                        return;

                    default:
                        success = false;
                        break;
                }
            } while (!success);
        }


        private void SeedGarage() // Todo: Ev. flytta till en egen klass
        {
            // Todo: Seed:a garaget med Vehicles
            _garage.Park(new Airplane("KDP523", 3, "Vit", 2));           
            _garage.Park(new Boat("DEF234", "Blå", 20.33m));
            _garage.Park(new Boat("LOL111", "Blå", 0.33m));
            _garage.Park(new Bus("ABC323", 8, "Grön", 25));
            _garage.Park(new Bus("FFS333", 6, "Grön", 10)); 
            _garage.Park(new Car("ABC123", 4, "Silver", FuelType.Gasoline));
            _garage.Park(new Car("SSS666", 4, "Rosa", FuelType.Diesel));
            _garage.Park(new Motorcycle("GHT423", 2, "Svart", 2000));
            _garage.Park(new Motorcycle("BBL888", 3, "Svart", 1500));
            _garage.Park(new Vehicle("KKK666", 0, "Generic veh. color"));
        }

        private void CreateMainMenu()  // Todo: Ev. flytta till en egen klass
        {

            while (true)
            {
                _ui.ClearScreen();

                _ui.WriteLine($"--------------------------------------------------------------------------");
                _ui.WriteLine($"Garaget har {_garage.Capacity} st parkeringsplatser (varav {_garage.Capacity - _garage.Count} är lediga.)");  // Todo: Lägg i Extensionmetod
                _ui.WriteLine($"--------------------------------------------------------------------------");

                _ui.WriteLine("Navigera i menyn med att mata in något av valen nedan (1, 2, 3 ,4, 5, 6, 0)"
                    + "\n1. Lista samtliga parkerade fordon"
                    + "\n2. Lista parkerade fordonstyper"
                    + "\n3. Lägg till fordon i garaget"
                    + "\n4. Ta ut fordon ur garaget"
                    + "\n5. Sök fordon via reg.nr"
                    + "\n6. Sök fordon via egenskaper"
                    + "\n0. Avsluta");

                char input = ' '; //Creates the character input to be used with the switch-case below.

                try
                {
                    input = _ui.ReadLine()![0]; //Tries to set input to the first char in an input line
                }
                catch (IndexOutOfRangeException) //If the input line is empty, we ask the users for some input.
                {
                    _ui.ClearScreen();
                    _ui.WriteLine("Var god mata in ett val!");
                }
                switch (input)
                {
                    case '1':
                        ListAllParkedVehicles();
                        break;
                    case '2':
                        ListParkedVehiclesByType();
                        break;
                    case '3':
                        ParkNewVehicle();
                        break;
                    case '4':
                        UnparkVehicle();
                        break;
                    case '5':
                        SearchByRegNo();
                        break;
                    case '6':
                        SearchByProperties();
                        break;
                    case '0':
                        Environment.Exit(0);
                        break;
                    default:
                        _ui.WriteLine("Var god ge ett giltigt val (0, 1, 2, 3, 4, 5, 6)");
                        break;
                }
            }
        }

        //Söker i _garage (aktuell instans av Garage<Vehicles>) efter Vehicle-objekt med angivena properties
        //Söker alltså efter fordon utifrån en egenskap eller flera ur basklassen Vehicle, där alla kombinationer är möjliga. 
        //Tex: Alla svarta fordon med fyra hjul. Alla motorcyklar som är rosa och har 3 hjul. Alla lastbilar. Alla röda fordon
        private void SearchByProperties()  // Todo: Göra om till meny och bryta ut sökningen?
        {
            VehicleType? vTypeChoice = null;
            int? vNoOfWheelsChoice = null;
            string? vColorChoice = null;

            string vehicleTypes = "";

            _ui.WriteLine("\nVälj sökparametrar:");

            foreach (VehicleType type in Enum.GetValues<VehicleType>())
            {
                vehicleTypes += $"\n{(int)type}. {type.ToStringSWE()}";
            }

            if (Utils.ReadYes("Vill du söka på fordonstyp?", _ui))
                vTypeChoice = Utils.ReadVehicleTypeAsInt($"Ange fordonstyp: {vehicleTypes}", _ui);

            if (Utils.ReadYes("Vill du söka på antal hjul?", _ui))
                vNoOfWheelsChoice = (int)Utils.ReadUnsignedInt("Ange antal hjul:", _ui);

            if (Utils.ReadYes("Vill du söka på färg?", _ui))
                vColorChoice = Utils.ReadString("Ange färg:", _ui);

            IEnumerable<Vehicle> searchResult = _garage.FindByProperties(vTypeChoice, vNoOfWheelsChoice, vColorChoice);

            if (searchResult.Count() == 0)
                _ui.WriteLine("Inga fordon hittade!");
            else
            {
                foreach (Vehicle vehicle in searchResult)
                {
                    _ui.WriteLine(vehicle.GetVehicleInfo());
                }
            }

            Utils.WaitAndClear(_ui);

        }

        // Söker i _garage (dvs aktuell instans av Garage<Vehicles>) efter Vehicle-objekt med angivet registreringsnumer 
        private void SearchByRegNo()
        {

            string regNo = Utils.ReadString("Ange registreringsnumret på fordonet ('0' - Avbryt):", _ui); // Todo: använd helper.metod för att kolla input

            regNo = String.Concat(regNo.Where(c => !Char.IsWhiteSpace(c)));  // Todo: Lägg i Extensionmetod

            if (regNo == "0")
                return;

            Vehicle? foundVehicle = _garage.FindVehicleByRegNumber(regNo);

            if (foundVehicle is not null)
            {
                _ui.WriteLine("Fordonet hittades!");
                _ui.WriteLine(foundVehicle.GetVehicleInfo());
            }
            else
            {
                _ui.WriteLine("Fordonet kunde inte hittas!");
            }

            Utils.WaitAndClear(_ui);
        }

        // Tar bort ett Vehicle objekt ur _garage (aktuell instans av Garage<Vehicle>), baserat på registreringsnumret 
        private void UnparkVehicle()
        {
            if (!_garage.IsEmpty)
            {
                bool isValidChoice = false;
                uint choice;
                string menuText;

                menuText = "1. Ta ut fordon ur garaget.\n0. Återgå till huvudmeny.";  // Todo: Ev. val att lista parkerade fordon här

                do
                {
                    choice = Utils.ReadUnsignedInt(menuText, _ui);
                    if (choice == 0)
                        return;

                    if (choice == 1)
                        isValidChoice = true;
                    else
                        _ui.WriteLine("Felaktigt val. Försök igen.");

                } while (!isValidChoice);

                string regNo = Utils.ReadString("Ange registreringsnumret på fordonet du vill ta ut ur garaget:", _ui); // Todo: kolla regnummer med egen helpermetod 

                var result = _garage.RemoveByRegNumber(regNo);

                if (result == UnparkResult.Success)
                    _ui.WriteLine("Fordonet är uttaget ur garaget!");
                else if (result == UnparkResult.RegNoNotFound)
                    _ui.WriteLine("Misslyckades. Garaget har inget fordon med angivet reg.nr");
            }
            else
            {
                _ui.WriteLine("Garaget är tomt!");
            }

            Utils.WaitAndClear(_ui);
        }

        // Skapar ett nytt Vehicle-objekt och lägger till det i _garage (aktuell instans av Garage<Vehicle>)
        private void ParkNewVehicle()
        {
            //if (!_garage.IsFull)
            //{
            //    bool isValidChoice = false;
            //    uint choice;
            //    string menuText;

            //    menuText = "1. Parkera nytt fordon.\n0. Återgå till huvudmeny.";

            //    do
            //    {
            //        choice = Utils.ReadUnsignedInt(menuText, _ui);
            //        if (choice == 0)
            //            return;

            //        if (choice == 1)
            //            isValidChoice = true;
            //        else
            //            _ui.WriteLine("Felaktigt val. Försök igen.");

            //    } while (!isValidChoice);

            //    menuText = "Välj typ av fordon vill du parkera: \n";

            //    // Skapa en meny utifrån enum VehicleType 
            //    foreach (VehicleType type in Enum.GetValues<VehicleType>())
            //    {
            //        menuText += $"{(int)type}. - {type.ToStringSWE()}\n"; // Menyvalen 1, 2, 3.. matchar enum VehicleType's bakomliggande värden
            //    }

            //    VehicleType vehicleType = Utils.ReadVehicleTypeString($"{menuText}", _ui);

            //    Vehicle? vehicle = CreateNewVehicle(vehicleType);

            //    if (vehicle is not null)
            //    {
            //        var result = _garage.Park(vehicle);

            //        if (result == ParkResult.Success)
            //            _ui.WriteLine("Ditt fordon är nu parkerat i garaget!");
            //        else if (result == ParkResult.AlreadyExists)
            //            _ui.WriteLine("Parkeringen misslyckades. Ett fordon med samma reg.nr finns redan i garaget.");
            //    }
            //}
            //else
            //{
            //    _ui.WriteLine("Garaget är fullt!");
            //}

            //Utils.WaitAndClear(_ui);


            if (_garage.IsFull)
            {
                _ui.WriteLine("Garaget är fullt!");
                Utils.WaitAndClear(_ui);
                return;
            }

            //Bygg upp en meny utifrån tillgängliga enum VehicleTypes
            var menu = new StringBuilder("Välj typ av fordon:\n");

            foreach (VehicleType type in Enum.GetValues<VehicleType>())
            {
                menu.AppendLine($"{(int)type}. {type.ToStringSWE()}");
            }

            menu.AppendLine("0. Avbryt");

            int choice;

            while (true)
            {
                choice = (int)Utils.ReadUnsignedInt(menu.ToString(), _ui);

                if (choice == 0)
                    return;

                if (Enum.IsDefined(typeof(VehicleType), choice))
                    break;

                _ui.WriteLine("Felaktigt val. Försök igen.");
            }

            //// Menyvalen 1, 2, 3.. matchar enum VehicleType's bakomliggande värden, kan då castas direkt till rätt VehicleType
            VehicleType selectedType = (VehicleType)choice;

            //Vehicle? vehicle = CreateNewVehicle(selectedType);

            Vehicle? vehicle = VehicleFactory.Create(selectedType, _ui); // Todo: Kolla om regnr redan finns i garaget innan man börjar skapa nytt fordon. Extensionmetod som anropas i factoryt? Eller en metod i Utils.

            if (vehicle is not null)
            {
                var result = _garage.Park(vehicle);

                if (result == ParkResult.Success)
                    _ui.WriteLine("Ditt fordon är nu parkerat i garaget!");
                else if (result == ParkResult.AlreadyExists)
                    _ui.WriteLine("Misslyckades. Fordon med detta reg.nr finns redan i garaget.");
                else
                    _ui.WriteLine("Misslyckades. Garaget är fullt.");
            }

            Utils.WaitAndClear(_ui);

        }

        // Skapar ett nytt Vehicle-objekt
        // Todo: Kan ev. brytas ut till VehicleFactory och delas upp i: ReadVehicleBaseInfo(), ReadSpecificVehicleInfo(vehicleType), VehicleFactory.Create() 
        //private Vehicle? CreateNewVehicle(VehicleType vehicleType)
        //{
        //    string regNo = Utils.ReadString("Ange registreringsnummer:", _ui);

        //    int noOfWheels = 0;

        //    if (vehicleType != VehicleType.Boat)
        //        noOfWheels = (int)Utils.ReadUnsignedInt("Ange antal hjul:", _ui);

        //    string color = Utils.ReadString("Ange färg:", _ui);

        //    Vehicle? vehicle = null;

        //    // Polymorfism - variabeln vehicle kan pekar på objekt av typen Vehicle + ärvande subklasser (Car, Boat etc).
        //    switch (vehicleType)
        //    {
        //        case VehicleType.Airplane:
        //            int noOfEngines = (int)Utils.ReadUnsignedInt("Ange antal motorer:", _ui);
        //            vehicle = new Airplane(regNo, noOfWheels, color, noOfEngines);
        //            break;
        //        case VehicleType.Boat:
        //            _ui.WriteLine("Ange längd:");
        //            decimal length = decimal.Parse(_ui.ReadLine());  // Todo: Fixa en Utils-metod för ReadDecimal()
        //            vehicle = new Boat(regNo, color, length);
        //            break;
        //        case VehicleType.Bus:
        //            int noOfSeats = Utils.ReadPositiveInt("Ange antal sittplatser:", _ui);
        //            vehicle = new Bus(regNo, noOfWheels, color, noOfSeats);
        //            break;
        //        case VehicleType.Car:
        //            FuelType fuel = Utils.ReadFuelTypeAsInt("Välj drivmedel:", _ui);                                   
        //            vehicle = new Car(regNo, noOfWheels, color, fuel);
        //            break;
        //        case VehicleType.Motorcycle:
        //            int cylinderVolume = Utils.ReadPositiveInt("Ange cylindervolym:", _ui);
        //            vehicle = new Motorcycle(regNo, noOfWheels, color, cylinderVolume);
        //            break;
        //        case VehicleType.Generic:
        //            vehicle = new Vehicle(regNo, noOfWheels, color);
        //            break;
        //        default:
        //            break;

        //    }

        //    return vehicle;
        //}

        private void ListAllParkedVehicles()
        {
            int counter = 0;
            _ui.WriteLine();

            _ui.WriteLine($"Det finns {_garage.Count} parkerade fordon i garaget:");

            foreach (Vehicle vehicle in _garage)
            {
                // Polymorfism - variabeln vehicle pekar på objekt av typen Vehicle + ärvande subklasser (Car, Boat etc).
                // Vehicle-klassens metod GetVehicleInfo() override:as i subklasserna -> Gör klassspecifika implementationer åtkomliga via variabeln vehicle

                _ui.WriteLine($"{++counter}. - {vehicle.GetVehicleInfo()}"); // - Typ: {vehicle.ToString()}");  // Todo: Ta bort, ska användas för debug
            }

            Utils.WaitAndClear(_ui);
        }

        private void ListParkedVehiclesByType()
        {
            _ui.WriteLine();

            //Todo: Ska man kunna välja här? Tex _ui.WriteLine("Vilken typ av fordon vill du lista?");?

            _ui.WriteLine($"Det finns {_garage.Count} parkerade fordon i garaget:");

            // För varje fordonstyp används LINQ för att räkna antalet av aktuell typ garaget, och skriver sedan ut antalet samt det svenska namnet på typen.

            foreach (VehicleType type in Enum.GetValues<VehicleType>())
            {
                //if (type == VehicleType.Generic)
                //    continue;

                int count = _garage.Count(v => v.VehicleType == type);

                _ui.WriteLine($"{type.ToStringSWE()}: {count} st.");
            }

            Utils.WaitAndClear(_ui);
        }

        // Todo: Debug.Writeline och return null vid catch-block i applikationen
        // Todo: Null-varningar för variabler kan släckas med typ y = x.color ?? : [default värde om null]
    }
}
