using Garage_1._0.src;

namespace Garage_1._0.Tests
{

    // Se till att bara köra isolerade delar av applikationen, små metoder som gör en sak typ, enheter, (units) körs här.
    // Todo: Döpa om till UtilTests?
    // Todo: för att metoder ska kunna testa måste klassen vara public (inte internal). Kan sättas med attribut [assembly(InternalVisible)..osv
    // Bättre lösning: Lägg till i projektfilen -> <ItemGroup><InternalVisibleTo Include="[projektnamn].Tests"/></ItemGroup>

    public class GarageTests
    {
        [Fact]
        public void Park_OneVehicleInGarage_ShouldReturnOne()
        {
            //Arrange
            const int expected = 1;
            
            // Kan i spelet vara typ Valid symbol, MaxHealth, InvalidSymbol, DefaultDamage osv längst upp i klassen
            

            Garage<Vehicle> garage = new Garage<Vehicle>(10);   
            // I Spelet skapas en egen TestCreature, eftersom Creatture är en Abstrakt klass.
            // Samt hjälpbibliotek för att testa. EJ HÅRDKODA I TEST

            // Todo: lägg till MockUI mui = new MockUI() här, mui.SetInput = expected

            Vehicle vehicle = new Vehicle("ABC123", 4, "White");

            //Act

            garage.Park(vehicle);

            //Assert

            Assert.Equal(expected, garage.Count);
        }

        // [Theory] testmetod som ska köras flera ggr. Sätter inline-data till testmetoden direkt och kan användas istället för arrange och för att testa med massor av testfall 
        // InlineData(argument, resultat) används istället för minMetod(int x) osv. Sedan anropas testmetoden direkt under med typ minTestmetod(argument, resultat), och sedan assert.Equal(resultat...
        // Om man förväntar sig samma resultat från en metod med flera olika anrop är theory lämpligt.
    }
}
