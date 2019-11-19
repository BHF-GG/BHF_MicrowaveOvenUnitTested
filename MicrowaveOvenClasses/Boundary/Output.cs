using MicrowaveOvenClasses.Interfaces;

namespace MicrowaveOvenClasses.Boundary
{
    public class Output : IOutput
    {
        public string OutputLine(string line)
        {
            System.Console.WriteLine(line);
            return line;
        }
        
    }
}