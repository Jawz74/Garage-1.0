using Garage_1._0.src;

namespace Garage_1._0.Helpers
{
    public static class Utils
    {

        public static string ReadString(string prompt, IUI ui)
        {
            string answer;
            bool success = false;

            do
            {
                ui.WriteLine($"{prompt}:");
                answer = ui.ReadLine();

                if (string.IsNullOrWhiteSpace(answer))
                {
                    ui.WriteLine($"Felaktig inmatning. Försök igen.");
                }
                else
                {
                    success = true;
                }
            }
            while (!success);

            return answer;
        }

        public static uint ReadUInt(string prompt, IUI ui)
        {
            do
            {

                string input = ReadString(prompt, ui);

                if (uint.TryParse(input, out uint result))
                {
                    return result;
                }
                else
                {
                    //ToDo: Write error message
                    ui.WriteLine($"Felaktig inmatning. Försök igen.");
                }
            }
            while (true);
        }
    }
}
