using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_1._0.src
{
    internal class Car : Vehicle
    {
        public FuelType Fuel { get; private set; } = FuelType.Gasoline;
        public Car(string registrationNumber, int numberOfWheels, string color, FuelType fuel ) : base(registrationNumber, numberOfWheels, color)
        {
            VehicleType = VehicleType.Car;
            Fuel = fuel;
        }

        public override string GetVehicleInfo()
        {
            return base.GetVehicleInfo() + $" Drivmedel: {Fuel.ToStringSWE()}";
        }
    }
}
