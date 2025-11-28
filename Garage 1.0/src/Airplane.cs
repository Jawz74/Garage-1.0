using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Garage_1._0.src
{
    internal class Airplane : Vehicle
    {
        public int NumberOfEngines { get; private set; }

        public Airplane(string registrationNumber, int numberOfWheels, string color, int numberOfEngines) : base(registrationNumber, numberOfWheels, color)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(NumberOfEngines);  // Antal motorer = 0 är ok, Segelflygplan etc. Men kasta fel om antalet motorer < 0
            VehicleType = VehicleType.Airplane;
            NumberOfEngines = numberOfEngines;
        }

        public override string GetVehicleInfo()
        {
            return base.GetVehicleInfo() + $" Antal motorer: {NumberOfEngines}";
        }
    }
}
