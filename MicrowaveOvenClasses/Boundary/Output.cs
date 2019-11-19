using System.Dynamic;
using MicrowaveOvenClasses.Interfaces;

namespace MicrowaveOvenClasses.Boundary
{
    public class Output : IOutput
    {
        public string OutTextTest { get; protected set; }

        public void OutputLine(string line)
        {
            System.Console.WriteLine(line);
            OutTextTest = line;
        }
    }
}