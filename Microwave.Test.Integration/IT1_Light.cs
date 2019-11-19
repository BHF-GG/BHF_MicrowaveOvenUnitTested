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
            _output = new Output();
            _sut = new Light(_output);
        }

        [Test]
        public void LightTurnsOn_WasOff_CorrectOutPutString()
        {
            _sut.TurnOn();
            Assert.That(_output.OutTextTest.Contains("Light is turned on"));
        }

        [Test]
        public void LightTurnsOff_WasOn_CorrectOutPutString()
        {
            _sut.TurnOn();
            _sut.TurnOff();
            _output.OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        [Test]
        public void TurnOn_WasOn_CorrectOutput()
        {
            _sut.TurnOn();
            _sut.TurnOn();
            _output.OutputLine(Arg.Is<string>(str => str.Contains("on")));
        }

        [Test]
        public void TurnOf_WasOff_CorrectOutput()
        {
            _sut.TurnOff();
            _sut.TurnOff();
            

           // _output.OutputLine(_output.Contains("jafbdasl")==true);
        }
    }
}
