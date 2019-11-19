using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrowaveOvenClasses.Interfaces
{
    public interface IOutput
    {
        string OutTextTest { get; }
        void OutputLine(string line);
    }
}
