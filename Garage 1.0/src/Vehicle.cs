using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage_1._0.src
{
    public class Vehicle : IVehicle
    // Todo: Ska och ärv från IVehicle
    {
        // Todo: protected setters för att ärvande object (tex bil , motoscykel, båt osv) ska kunna sätta sina properties själva
        // Todo: Kom ihåg att klasser kan ärva från flera interface samtidigt, kanske är användbart i Fordonsklasserna (typ IWheelVehicle eller nåt)?

        public VehicleType VehicleType { get; protected set; } = VehicleType.Generic;
        public string RegistrationNumber { get; private set; }  // Private set gör att Propertyn kan sättas från andra ställen i denna klass än i konstruktorn 
        public int NumberOfWheels { get; private set; }
        public string Color { get; private set; } = "N/A";  // Todo: Alternativt = null eller string? (men då får man en varning vid användning i koden sedan)


        public Vehicle(string registrationNumber, int numberOfWheels, string color)   // Todo: Högerklick > Qick actions > Add nul check osv
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(registrationNumber);

            if (numberOfWheels < 0)
                throw new ArgumentOutOfRangeException(nameof(numberOfWheels));

            RegistrationNumber = registrationNumber;
            NumberOfWheels = numberOfWheels;
            Color = color;
        }

        public virtual string GetVehicleInfo()
        {
            return $"Fordon: {VehicleType.ToStringSWE()} " +
                   $"Reg.nr: {RegistrationNumber} " +
                   $"Hjul: {NumberOfWheels} " +
                   $"Färg: {Color}";
        }

        public override string ToString()
        {
            return GetType().Name;
        }

    }


}
