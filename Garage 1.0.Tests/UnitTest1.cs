using Garage_1._0.src;

namespace Garage_1._0.Tests
{

    // Todo: för att metoder ska kunna testa måste klassen vara public (inte internal). Kan sättas med attribut [assembly(InternalVisible)..osv
    // Bättre lösning: Lägg till i projektfilen -> <ItemGroup><InternalVisibleTo Include="[projektnamn].Tests"/></ItemGroup>
    // Todo: [Theory] testmetod som ska köras flera ggr. Sätter inline-data till testmetoden direkt och kan användas istället för arrange och för att testa med massor av testfall 
    // InlineData(argument, resultat) används istället för minMetod(int x) osv. Sedan anropas testmetoden direkt under med typ minTestmetod(argument, resultat), och sedan assert.Equal(resultat...
    // Om man förväntar sig samma resultat från en metod med flera olika anrop är theory lämpligt.

    // I Spelet skapas en egen TestCreature, eftersom Creatture är en Abstrakt klass.
    // Samt hjälpbibliotek för att testa. EJ HÅRDKODA I TEST

    // Todo: lägg till MockUI mui = new MockUI() här, mui.SetInput = expected

    // Kör bara isolerade delar av applikationen, små metoder som gör en sak typ, enheter, (units) körs här.

    //public class GarageTests
    //{
    //    [Fact]
    //    public void Park_OneVehicleInGarage_ShouldReturnOne()
    //    {
    //        //Arrange
    //        const int expected = 1;

    //        // Kan i spelet vara typ Valid symbol, MaxHealth, InvalidSymbol, DefaultDamage osv längst upp i klassen


    //        Garage<Vehicle> garage = new Garage<Vehicle>(10);


    //        Vehicle vehicle = new Vehicle("ABC123", 4, "White");

    //        //Act

    //        garage.Park(vehicle);

    //        //Assert

    //        Assert.Equal(expected, garage.Count);
    //    }

    public class GarageTests
    {

        //  Park()
        [Fact]
        public void Park_Should_Add_Vehicle_When_Space_Available()
        {
            // Arrange
            var garage = new Garage<Vehicle>(2);
            var car = new Car("ABC123", 4, "Silver", FuelType.Gasoline);

            // Act
            var result = garage.Park(car);

            // Assert
            Assert.Equal(ParkResult.Success, result);
            Assert.Equal(1, garage.Count);
        }

        [Fact]
        public void Park_Should_Return_GarageFull_When_No_Space()
        {
            // Arrange
            var garage = new Garage<Vehicle>(1);
            garage.Park(new Car("AAA111", 4, "Röd", FuelType.Gasoline));

            var car2 = new Car("BBB222", 4, "Blå", FuelType.Gasoline);

            // Act
            var result = garage.Park(car2);

            // Assert
            Assert.Equal(ParkResult.GarageFull, result);
            Assert.Equal(1, garage.Count);
        }

        [Fact]
        public void Park_Should_Return_AlreadyExists_When_RegNumber_Duplicate()
        {
            // Arrange
            var garage = new Garage<Vehicle>(2);
            garage.Park(new Car("ABC123", 4, "Röd", FuelType.Gasoline));

            var duplicate = new Car("ABC123", 4, "Blå", FuelType.Gasoline);

            // Act
            var result = garage.Park(duplicate);

            // Assert
            Assert.Equal(ParkResult.AlreadyExists, result);
        }


        //  Remove()

        [Fact]
        public void Remove_Should_Remove_Vehicle_When_Exists()
        {
            // Arrange
            var garage = new Garage<Vehicle>(2);
            garage.Park(new Car("ABC123", 4, "Röd", FuelType.Gasoline));

            // Act
            var result = garage.RemoveByRegNumber("ABC123");

            // Assert
            Assert.Equal(UnparkResult.Success, result);
            Assert.True(garage.IsEmpty);
        }

        [Fact]
        public void Remove_Should_Return_RegNoNotFound_When_Not_Exists()
        {
            // Arrange
            var garage = new Garage<Vehicle>(2);
            garage.Park(new Car("AAA111", 4, "Röd", FuelType.Gasoline));

            // Act
            var result = garage.RemoveByRegNumber("XYZ999");

            // Assert
            Assert.Equal(UnparkResult.RegNoNotFound, result);
        }

        [Fact]
        public void Remove_Should_Return_GarageEmpty_When_Empty()
        {
            // Arrange
            var garage = new Garage<Vehicle>(1);

            // Act
            var result = garage.RemoveByRegNumber("ABC123");

            // Assert
            Assert.Equal(UnparkResult.GarageEmpty, result);
        }


        //FindVehicleByRegNumber()
        [Fact]
        public void Find_Should_Return_Vehicle_When_Exists()
        {
            var garage = new Garage<Vehicle>(1);
            var car = new Car("ABC123", 4, "Red", FuelType.Gasoline);
            garage.Park(car);

            var result = garage.FindVehicleByRegNumber("ABC123");

            Assert.NotNull(result);
            Assert.Equal("ABC123", result!.RegistrationNumber);
        }

        [Fact]
        public void Find_Should_Return_Null_When_Not_Exists()
        {
            var garage = new Garage<Vehicle>(1);

            var result = garage.FindVehicleByRegNumber("XYZ999");

            Assert.Null(result);
        }


        //  FindByProperties()

        [Fact]
        public void FindByProperties_Should_Find_By_Type()
        {
            var garage = new Garage<Vehicle>(5);
            garage.Park(new Car("A1", 4, "Röd", FuelType.Gasoline));
            garage.Park(new Boat("B1", "Blå", 10));

            var result = garage.FindByProperties(type: VehicleType.Car);

            Assert.Single(result);
            Assert.Equal("A1", result.First().RegistrationNumber);
        }

        [Fact]
        public void FindByProperties_Should_Find_By_Color()
        {
            var garage = new Garage<Vehicle>(5);
            garage.Park(new Car("A1", 4, "Röd", FuelType.Gasoline));
            garage.Park(new Car("B1", 4, "Blå", FuelType.Gasoline));

            var result = garage.FindByProperties(color: "Blå");

            Assert.Single(result);
            Assert.Equal("B1", result.First().RegistrationNumber);
        }

        [Fact]
        public void FindByProperties_Should_Find_By_Multiple_Properties()
        {
            var garage = new Garage<Vehicle>(5);
            garage.Park(new Car("A1", 4, "Röd", FuelType.Gasoline));
            garage.Park(new Car("B1", 4, "Röd", FuelType.Gasoline));
            garage.Park(new Boat("C1", "Röd", 10));

            // Röda bilar med 4 hjul
            var result = garage.FindByProperties(
                type: VehicleType.Car,
                noOfWheels: 4,
                color: "Röd"
            );

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void FindByProperties_Should_Return_Empty_When_No_Match()
        {
            var garage = new Garage<Vehicle>(5);

            var result = garage.FindByProperties(color: "Transparent");

            Assert.Empty(result);
        }


        //  Properties
        [Fact]
        public void IsFull_Should_Be_True_When_Garage_Is_Full()
        {
            var garage = new Garage<Vehicle>(1);
            garage.Park(new Car("A1", 4, "Röd", FuelType.Gasoline));

            Assert.True(garage.IsFull);
        }

        [Fact]
        public void IsEmpty_Should_Be_True_When_Garage_Is_Empty()
        {
            var garage = new Garage<Vehicle>(1);

            Assert.True(garage.IsEmpty);
        }

        [Fact]
        public void Count_Should_Return_Number_Of_Vehicles()
        {
            var garage = new Garage<Vehicle>(3);
            garage.Park(new Car("A1", 4, "Röd", FuelType.Gasoline));
            garage.Park(new Car("B1", 4, "Blå", FuelType.Gasoline));

            Assert.Equal(2, garage.Count);
        }
    }

}
