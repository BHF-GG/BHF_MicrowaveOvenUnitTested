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
    [TestFixture]
    public class IT1_Light
    {
        private Light _sut;
        private IOutput _output;

        [SetUp]
        public void SetUp()
        {
            //Testing with a concrete object instead of Substitute
            _output = Substitute.For<IOutput>();
            _sut = new Light(_output);
        }

        [Test]
        public void LightTurnsOn_WasOff_CorrectOutPutString()
        {
            _sut.TurnOn();

            //Assert
            _output.Received().OutputLine(Arg.Is<string>(x =>
                x == "Light is turned on"));
        }

        [Test]
        public void LightTurnsOff_WasOn_CorrectOutPutString()
        {
            _sut.TurnOn();
            _sut.TurnOff();

            //Assert
            _output.Received().OutputLine(Arg.Is<string>(x =>
                x == "Light is turned off"));
        }

        [Test]
        public void TurnOn_WasOn_CorrectOutput()
        {
            _sut.TurnOn();
            _sut.TurnOn();

            //Assert
            _output.Received().OutputLine(Arg.Is<string>(x =>
                x == "Light is turned on"));
        }

        [Test]
        public void TurnOff_WasOff_CorrectOutput()
        {
            //Have to turn on, and then turn off twice to check this - because isOn (boolean) is set to false at start
            _sut.TurnOn();
            _sut.TurnOff();
            _sut.TurnOff();

            //Assert
            _output.Received().OutputLine(Arg.Is<string>(x =>
                x == "Light is turned off"));
        }
    }
}
