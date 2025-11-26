using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_1._0.src
{
    internal class Motorcycle : Vehicle
    {
        public int CylinderVolume { get; private set; }
        public Motorcycle(string registrationNumber, int numberOfWheels,  string color, int cylinderVolume) : base(registrationNumber, numberOfWheels, color)
        {
            VehicleType = VehicleType.Motorcycle;
            CylinderVolume = cylinderVolume;
        }

        public override string GetVehicleInfo()
        {
            return base.GetVehicleInfo() + $" Cylindervolym: {CylinderVolume}";
        }
    }
}
