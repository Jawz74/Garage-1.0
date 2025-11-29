namespace Garage_1._0.src
{
    // Interface:t definierar minsta uppsättning UI-funktioner programmet behöver för in- och utmatning av text.
    // Klasser som använder interfacet arbetar mot dessa abstrakta metoder istället för mot konkreta UI-klasser direkt, (som Console).
    // Möjliggör istället utbytbara UI-implementationer (tex konsol, fil, test-mock), vilket gör programmet löst kopplat, testbart och enkelt att utöka.

    //Todo: Lägg denna och ärvande klasser i underprojekt .Abstractions, Getinput() och SetInput() här?

    public interface IUI
    {
        void ClearScreen();
        int Read();
        string ReadLine();
        void Write(string text = "");
        void WriteLine(string text = "");
    }
}