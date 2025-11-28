using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_1._0.src
{
    internal static class VehicleTypeExtensions
    {
        public static string ToStringSWE(this VehicleType vehicleType)
        {
            switch (vehicleType)
            {
                case VehicleType.Car:
                    return "Bil";
                case VehicleType.Airplane:
                    return "Flygplan";    
                case VehicleType.Bus:
                    return "Buss";
                case VehicleType.Motorcycle:
                    return "Motorcykel";
                case VehicleType.Boat:
                    return "Båt";
                case VehicleType.Generic:
                    return "Övrigt";
                default:
                    return vehicleType.ToString();
            }
        }

    }

    internal static class FuelTypeExtensions   
    {
        public static string ToStringSWE(this FuelType fuelType)
        {
            switch (fuelType)
            {
                case FuelType.Gasoline:
                    return "Bensin";
                case FuelType.Diesel:
                    return "Diesel";
                case FuelType.Other:
                    return "Annat";
                default:
                    return fuelType.ToString();
            }
        }
    }
}
