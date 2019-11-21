using System;
using System.Collections.Generic;
using System.IO;
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
        private Output _output;

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
            string consoleOutput;
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);

                _sut.TurnOn();

                consoleOutput = stringWriter.ToString();
            }

            Assert.That(consoleOutput, Is.EqualTo("Light is turned on\r\n"));
        }

        [Test]
        public void LightTurnsOff_WasOn_CorrectOutPutString()
        {
            string consoleOutput;
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);

                _sut.TurnOn();
                _sut.TurnOff();

                consoleOutput = stringWriter.ToString();
            }

            Assert.That(consoleOutput, Is.EqualTo("Light is turned on\r\nLight is turned off\r\n"));
        }

        [Test]
        public void TurnOn_WasOn_CorrectOutput()
        {
            string consoleOutput;
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);

                _sut.TurnOn();
                _sut.TurnOn();

                consoleOutput = stringWriter.ToString();
            }

            Assert.That(consoleOutput, Is.EqualTo("Light is turned on\r\n"));
        }

        [Test]
        public void TurnOff_WasOff_CorrectOutput()
        {
            string consoleOutput;
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);

                //Have to turn on, and then turn off twice to check this - because isOn (boolean) is set to false at start
                _sut.TurnOn();
                _sut.TurnOff();
                _sut.TurnOff();

                consoleOutput = stringWriter.ToString();
            }

            Assert.That(consoleOutput, Is.EqualTo("Light is turned on\r\nLight is turned off\r\n"));

        }
    }
}
