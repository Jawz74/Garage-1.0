using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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

    internal class GarageHandler      {
        // Todo: Skapa jsonfil "appsettings.json" med nycklar för ui + ev. garagestorlek, etc.  (garage {ui = console} }
        // Add -> Json Configuration file. När den är klar: Högerklick -> Properties -> 'Copy if newer' lägger till den i 
        // Lägg till using Microsoft.Extensions.COnfiguration
        // Sedan läs ut värden med:
        // IConfiguration config = new ConfigurationBuilder() // Builder pattern för logging. Finns i nuget: Microsoft .Extensions.Configuration.FileProviders, .Json, 
        // .SetBasePath(Environment.CurrentDirectory.AddJsonFile("appsettings.json", optional:false, reloadOnChange: true).Build()
        // var ui = config.GetSection("garage:ui").Value;
        // if ui = console { IUI = new ConsoleUI ...}

        private IUI _ui;                        // Todo: Vill man kunna sätta om iui här på annat sätt än genom konstruktorn?

        protected Garage<Vehicle> _garage = null!;        // Todo: Denna bör initieras, antingen i konstruktorn eller hårdkodat (annars får man varningar överallt). Hur ska man tänka här?

        //public IEnumerable<Vehicle> Garage { get; private set; }
        public GarageHandler(IUI ui)            // Todo: Kasta ArgumentException.ThrowIfNullOrWhitespace osv för att skydda mot fel värden i konstruktorer. Även om utility-klasser kollar inmatningar.
        {
            ArgumentNullException.ThrowIfNull(ui);
            _ui = ui;
            //_garage = new Garage<Vehicle>(capacity: 10); // Todo: Ska denna instans skapas här? Byt ut hårdkodat argument Capacity mot  defaultvärde ur en config-fil då kanske? 
        }

        internal void Run()   
        {
            CreateGarage();
            SeedGarage();
            CreateMainMenu();
        }

        private void CreateGarage()
        {
            int input;
            bool success = false;

            do
            {
              _ui.WriteLine("Ange antalet platser i garaget:");
              input = int.Parse(_ui.ReadLine());   // Todo: lägg i helperklass, typ ReadInt() -> int.TryParse(capacity, out int result);
                success = true;                     // Todo: fixa loopen med felhantering och kanske val om att köra Seed()
            } while (!success);

            _garage = new Garage<Vehicle>(capacity: input);            
        }

        private void SeedGarage() // Todo: Ev. flytta till en egen klass
        {             
            // Todo: Seed:a garaget med Vehicles
            _garage.Park(new Car("ABC123",  4, "Silver", FuelType.Gasoline));
            _garage.Park(new Boat("DEF223", "Bå", 20.33m));
            _garage.Park(new Bus("ABC323", 4, "Grön", 25));
            _garage.Park(new Motorcycle("ABC423", 4, "Svart", 2000));
            _garage.Park(new Airplane("ABC523", 3, "Vit", 2));
            _garage.Park(new Car("SSS666", 4, "Röd", FuelType.Diesel));

            //_garage.RemoveByRegNumber("ABC323");
        }

        private void CreateMainMenu()  // Todo: Ev. flytta till en egen klass
        {
            _ui.ClearScreen();

            while (true)
            {
                _ui.WriteLine("Navigera i menyn med att mata in något av valen \n(1, 2, 3 ,4, 5, 6, 0)"
                    + "\n1. Lista samtliga parkerade fordon"
                    + "\n2. Lista parkerade fordonstyper"
                    + "\n3. Lägg till fordon i garaget"
                    + "\n4. Fyra"
                    + "\n5. Fem"
                    + "\n6. Sex"
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
                       
                        break;
                    case '6':
                        
                        break;
                    case '0':
                        Environment.Exit(0);
                        break;
                    default:
                        _ui.WriteLine("Please enter some valid input (0, 1, 2, 3, 4)");
                        break;
                }
            }
        }

        private void UnparkVehicle()
        {
            if (!_garage.IsEmpty)
            {
                _ui.WriteLine("Ange registreringsnumret till fordonet du vill ta ut ur garaget:");
                string regNo = _ui.ReadLine();   // Todo: använd helper.metod för att kolla input

                if (_garage.RemoveByRegNumber(regNo) == UnparkResult.Success)
                    _ui.WriteLine("Fordonet är uttaget ur garaget!");
                else if (_garage.RemoveByRegNumber(regNo) == UnparkResult.RegNoNotFound)
                    _ui.WriteLine("Misslyckades. Garaget har inget fordon med angivet reg.nr");
                //else if (_garage.RemoveByRegNumber(regNo) == UnparkResult.GarageEmpty)
                //    _ui.WriteLine("Misslyckades. Inga fordon finns i garaget.");
            }
            else
            {
                _ui.WriteLine("Garaget är tomt!");
            }
        }

        private void ParkNewVehicle()
        {
            if(!_garage.IsFull)
            {
                bool isValidChoice = false;
                int counter, choice; 
                _ui.WriteLine("Välj typ av fordon vill du parkera:");

                do
                {
                    counter = 1;

                    foreach (VehicleType type in Enum.GetValues<VehicleType>())
                    {
                        _ui.WriteLine($"{counter++}. - {type.ToStringSWE()}");
                    }

                    _ui.WriteLine("0. - Avsluta");

                    isValidChoice = int.TryParse(_ui.ReadLine(), out choice);   // Todo: använd helper.metod för att kolla input

                    if (isValidChoice)
                    {
                        if (choice == 0)
                            return;

                        isValidChoice = Enum.IsDefined(typeof(VehicleType), choice) ? true : false;
                    }

                    if(!isValidChoice)                    
                     _ui.WriteLine("Felaktigt val. Försök igen");

                } while (!isValidChoice);

                // Menyvalen ligger +1 mot enum VehicleType's bakomliggande värden
                var vTypesList = Enum.GetValues<VehicleType>().ToList();
                VehicleType vehicleType = vTypesList[choice - 1];      

                Vehicle? vehicle = CreateNewVehicle(vehicleType);

                if (vehicle is not null)
                {
                    if (_garage.Park(vehicle) == ParkResult.Success)
                        _ui.WriteLine("Ditt fordon är nu parkerat i garaget!");
                    else if (_garage.Park(vehicle) == ParkResult.AlreadyExists)
                        _ui.WriteLine("Parkeringen misslyckades. Ett fordon med samma reg.nr finns redan i garaget.");
                    //else if (_garage.Park(vehicle) == ParkResult.GarageFull)
                    //    _ui.WriteLine("Parkeringen misslyckades. Garaget är redan fullt.");
                }
            }
            else
            {
                _ui.WriteLine("Garaget är fullt!");
            }
        }

        // Todo: Metoden bör delas upp i: ReadVehicleBaseInfo(), ReadSpecificVehicleInfo(vehicleType), VehicleFactory.Create(...) 
        private Vehicle? CreateNewVehicle(VehicleType vehicleType)  // Todo: Kan ev. brytas ut till VehicleFactory
        {

            _ui.WriteLine("Ange registreringsnummer:");
            string regNo = _ui.ReadLine();   // Todo: använd helper.metod för att kolla input

            int noOfWheels = 0;
            if (vehicleType != VehicleType.Boat)
            { 
                _ui.WriteLine("Ange antal hjul:");
                noOfWheels = int.Parse(_ui.ReadLine());   // Todo: använd helper.metod för att kolla input
            }

            _ui.WriteLine("Ange färg:");
            string color = _ui.ReadLine();   // Todo: använd helper.metod för att kolla input

            Vehicle? vehicle = null; 

            switch (vehicleType)
            {
                case VehicleType.Airplane:
                    _ui.WriteLine("Ange antal motorer:");
                    int noOfEngines = int.Parse(_ui.ReadLine());
                    vehicle = new Airplane(regNo, noOfWheels, color, noOfEngines);
                    break;
                case VehicleType.Boat:
                    _ui.WriteLine("Ange längd:");
                    decimal length = decimal.Parse(_ui.ReadLine());
                    vehicle = new Boat(regNo, color, length );
                    break;
                case VehicleType.Bus:
                    _ui.WriteLine("Ange antal säten:");
                    int noOfSeats = int.Parse(_ui.ReadLine());
                    vehicle = new Bus(regNo, noOfWheels, color, noOfSeats);
                    break;
                case VehicleType.Car:
                    _ui.WriteLine("Ange drivmedel:");
                    FuelType fuelType = (FuelType)int.Parse(_ui.ReadLine());  // Todo: fixa en meny här
                    vehicle = new Car(regNo, noOfWheels, color, fuelType);
                    break;
                case VehicleType.Motorcycle:
                    _ui.WriteLine("Ange antal cylindervolym:");
                    int cylinderVolume = int.Parse(_ui.ReadLine());
                    vehicle = new Motorcycle(regNo, noOfWheels, color, cylinderVolume);
                    break;
                case VehicleType.Generic:
                    vehicle = new Vehicle(regNo, noOfWheels, color);
                    break;
                default:
                    break;

            }

            return vehicle;
        }

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

            _ui.ReadLine();
        }

        private void ListParkedVehiclesByType()
        {
            _ui.WriteLine();

            //Todo: Ska man kunna välja här? Tex _ui.WriteLine("Vilken typ av fordon vill du lista?");?

            _ui.WriteLine($"Det finns {_garage.Count} parkerade fordon i garaget:");

            // För varje fordonstyp används LINQ för att räkna antalet av denna typ garaget, skriv ut det svenska namnet på- och antalet av typen.

            foreach (VehicleType type in Enum.GetValues<VehicleType>())  
            {
                //if (type == VehicleType.Generic)
                //    continue;

                int count = _garage.Count(v => v.VehicleType == type);

                _ui.WriteLine($"{type.ToStringSWE()}: {count} st.");
            }

            _ui.ReadLine();
        }

        // Todo: Debug.Writeline och return null vid catch-block i applikationen
        // Todo: Null-varningar för variabler kan släckas med typ y = x.color ?? : [default värde om null]

    }
}
