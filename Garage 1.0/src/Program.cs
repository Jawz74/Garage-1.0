namespace Garage_1._0.src
{
    internal class Program
    {
        // Todo: ska ConsoleUI ta emot en Log också kanske?

        //Todo: Det ska finnas en Garage Manager som använder Handler:IHandler, även ett IGarage ska finnas..

        static void Main(string[] args)
        {
            IUI _ui = new ConsoleUI();
            GarageHandler _garageHandler = new GarageHandler(_ui);
            _garageHandler.Run();
        }


        // Allmänna tips
        // Todo: Kom ihåg att endast publika metoder kan Mockas, bryt därför ut med detta i åtanke
        // Todo: Om samma sak görs på 3 eller fler ställen i koden -> Bryt ut till egna funktioner, klasser el dyl

        // Todo: Kom ihåg att så fort man vill göra nåt med en property i en getter eller setter bör man ha ett privat fält.
        // Tex ArgumentNullException.ThrowIfNullOrWhiteSpace(value, (nameof(value)); innnan man sätter fältet till value,
        // Man kan då även lägga till [MemberNotNull(nameof(_myField))] på settern för att få bort null-varning i konstruktorns tilldelning av värdet.
        // I 99% av fallen bör man ha fält privata och properties publika (med ev privata setters)

        // Todo: Rena värdetyper kan läggas i en struct, som sen används som properties i klasser (tex Position i Spelet). Dock ovanligt att använda structs.

        // Todo: Eventuellt lägga till Extensionmetoder (på collections tex). Görs i egen klass i egen folder och namespace. Tre krav: Statisk klass, statisk metod och this i parameterlistan.

        // Todo: Organisera filerna i projektet med mappar, för liknande funktionalitet. I Spelet: Extensions, GameWorld (Map, Call, Direction, Position), Characters (Creature, Player),
        // UI (ConsoleUI) osv. Namespace byts aut. Här src och tests för att innehålla huvudprojektet och testprojektet.
        // Egendefinierad typ LimitedList<T> läggs i eget pojekt (.dll) i Spelet. Egna Helper-klasser (eller från andra projekt) också.
    }
}
