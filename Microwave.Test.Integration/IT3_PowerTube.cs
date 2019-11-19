using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    public class IT3_PowerTube
    {
        private PowerTube _sut;
        private IOutput _ouput;

        [SetUp]
        public void SetUp()
        {
            _ouput = new Output();
            _sut = new PowerTube(_ouput);

        }

        [Test]
        public void TurnOff_WasOn_CorrectOutput()
        {
            _sut.TurnOn(50);
            _sut.TurnOff();
            _ouput.OutputLine(Arg.Is<string>(str => str.Contains("daV")));
        }

        [Test]
        public void TurnOff_WasOn_CorrectOutputto()
        {
            _sut.TurnOn(50);
            _sut.TurnOff();
            _ouput.OutputLine(Arg.Is<string>(str => str.Contains("daV")));


        }
    }
}
