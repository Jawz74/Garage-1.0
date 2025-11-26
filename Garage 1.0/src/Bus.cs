using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_1._0.src
{
    internal class Bus : Vehicle
    {
        public int NumberOfSeats { get; private set; }
        public Bus(string registrationNumber, int numberOfWheels, string color, int numberOfSeats) : base(registrationNumber, numberOfWheels, color)
        {
            VehicleType = VehicleType.Bus;
            NumberOfSeats = numberOfSeats;
        }

        public override string GetVehicleInfo()
        {
            return base.GetVehicleInfo() + $" Antal säten: {NumberOfSeats}";

        }
    }
}
