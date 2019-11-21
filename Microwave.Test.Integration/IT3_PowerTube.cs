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
    public class IT3_PowerTube
    {
        private PowerTube _powerTube;
        private IOutput _output;

        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _powerTube = new PowerTube(_output);

        }

        [Test]
        public void TurnOn_CorrectOutput()
        {
            string consoleOutput;
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);

                _powerTube.TurnOn(50);

                consoleOutput = stringWriter.ToString();
            }

            Assert.That(consoleOutput, Is.EqualTo("PowerTube works with 50 %\r\n"));
        }

        [Test]
        public void TurnOff_WasOn_CorrectOutput()
        {
            string consoleOutput;
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);

                _powerTube.TurnOn(50);
                _powerTube.TurnOff();

                consoleOutput = stringWriter.ToString();
            }

            Assert.That(consoleOutput, Is.EqualTo("PowerTube works with 50 %\r\nPowerTube turned off\r\n"));
        }

   
    }
}
