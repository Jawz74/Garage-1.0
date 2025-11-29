namespace Garage_1._0.src
{
    public class Vehicle : IVehicle
    {
        // Klassen innehåller protected setters för att ärvande object (tex bil , motoscykel, båt osv) ska kunna sätta sina properties själva
        // Todo: Kom ihåg att klasser kan ärva från flera interface samtidigt, kanske är användbart i Fordonsklasserna (typ IWheelVehicle eller nåt)?

        public VehicleType VehicleType { get; protected set; } = VehicleType.Generic;
        public string RegistrationNumber { get; private set; }  // Private set gör att Propertyn kan sättas från andra ställen i denna klass än i konstruktorn 
        public int NumberOfWheels { get; private set; }
        public string Color { get; private set; }  // Todo: Alternativt = null eller string? (men då får man en varning vid användning i koden sedan)


        public Vehicle(string registrationNumber, int numberOfWheels, string color = "N/A")   // Todo: Högerklick > Qick actions > Add nul check osv
        {
            // Om regnr saknas eller antal hjul <= 0, kasta fel.
            ArgumentException.ThrowIfNullOrWhiteSpace(registrationNumber);   // Todo: Måste hantera kastade fel någonstans - var ?
            ArgumentOutOfRangeException.ThrowIfNegative(numberOfWheels);    // Todo: kasta exceptions med if .. och throw new för mer information i meddelandet 

            RegistrationNumber = registrationNumber.ToUpper();
            NumberOfWheels = numberOfWheels;
            Color = string.IsNullOrWhiteSpace(color) ? "N/A" : color;
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
