using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrowaveOvenClasses.Interfaces
{
    public interface ICookController
    {
        void SetUi(IUserInterface ui);
        void StartCooking(int power, int time);
        void Stop();
    }
}
