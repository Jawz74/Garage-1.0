namespace Garage_1._0.src
{
    public class Boat : Vehicle
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
