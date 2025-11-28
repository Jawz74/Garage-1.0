using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using Garage_1._0.src;

namespace Garage_1._0.src.Factories
{
    internal static class VehicleFactory
    {
        // Publik enda entrypoint
        public static Vehicle? Create(VehicleType vehicleType, IUI ui)
        {
            // Hämta basinfo
            VehicleBaseInfo baseInfo = GetBaseClassProperties(vehicleType, ui);

            string regNo = baseInfo.RegistrationNumber;
            int noOfwheels = baseInfo.NumberOfWheels;
            string color = baseInfo.Color;          
           
            Vehicle? vehicle = null;

            // Hämta objektspecifik info och skapa rätt objekt
            switch (vehicleType)
            {
                case VehicleType.Airplane:
                    vehicle = CreateAirplane(regNo, noOfwheels, color, ui);
                    break;
                case VehicleType.Boat:
                    vehicle = CreateBoat(regNo, color, ui);
                    break;
                case VehicleType.Bus:
                    vehicle = CreateBus(regNo, noOfwheels, color, ui);
                    break;
                case VehicleType.Car:
                    vehicle = CreateCar(regNo, noOfwheels, color, ui);
                    break;
                case VehicleType.Motorcycle:
                    vehicle = CreateMotorcycle(regNo, noOfwheels, color, ui);
                    break;
                case VehicleType.Generic:
                    vehicle = new Vehicle(regNo, noOfwheels, color);
                    break;
                default:
                    throw new NotSupportedException($"Okänd fordonstyp!: {vehicleType}");                    
            }

            return vehicle;
        }


        // Sätter gemensamma properties, för alla typer av Vehicles 
        private static VehicleBaseInfo GetBaseClassProperties(VehicleType type, IUI ui)
        {
            string regNo = Utils.ReadString("Ange registreringsnummer:", ui).ToUpper();

            int wheels = 0;
            if (type != VehicleType.Boat)
                wheels = (int)Utils.ReadUnsignedInt("Ange antal hjul:", ui);

            string color = Utils.ReadString("Ange färg:", ui);

            return new VehicleBaseInfo
            {
                RegistrationNumber = regNo,
                NumberOfWheels = wheels,
                Color = color
            };
        }


        // Sätter properties för specifika Vehicles-objekt 
        private static Airplane CreateAirplane(string regNo, int wheels, string color, IUI ui)
        {
            int engines = (int)Utils.ReadUnsignedInt("Ange antal motorer:", ui);
            return new Airplane(regNo, wheels, color, engines);
        }

        private static Boat CreateBoat(string regNo, string color, IUI ui)
        {
            decimal length = Utils.ReadDecimal("Ange längd:", ui);
            return new Boat(regNo, color, length);
        }

        private static Bus CreateBus(string regNo, int wheels, string color, IUI ui)
        {
            int seats = (int)Utils.ReadUnsignedInt("Ange antal sittplatser:", ui);
            return new Bus(regNo, wheels, color, seats);
        }

        private static Car CreateCar(string regNo, int wheels, string color, IUI ui)
        {
            FuelType fuel = Utils.ReadFuelTypeAsInt("Välj drivmedel:", ui);
            return new Car(regNo, wheels, color, fuel);
        }

        private static Motorcycle CreateMotorcycle(string regNo, int wheels, string color, IUI ui)
        {
            int volume = (int)Utils.ReadUnsignedInt("Ange cylindervolym:", ui);
            return new Motorcycle(regNo, wheels, color, volume);
        }
    }

    public class VehicleBaseInfo
    {
        public required string RegistrationNumber { get; set; }
        public int NumberOfWheels { get; set; }
        public string Color { get; set; } = "N/A";
    }
}


