using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_1._0.src
{
    internal class Boat : Vehicle
    {
        public decimal Length { get; private set; }
        public Boat(string registrationNumber, string color, decimal length) : base(registrationNumber, 0, color)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(length);  // Längd <= 0 kastar ett fel

            VehicleType = VehicleType.Boat;
            Length = length;            
        }

        public override string GetVehicleInfo()
        {
            return base.GetVehicleInfo() + $" Längd: {Length}";
        }
    }
}
