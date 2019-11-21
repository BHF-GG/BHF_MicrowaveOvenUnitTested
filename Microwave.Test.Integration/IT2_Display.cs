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
    public class IT2_Display
    {
        private Display _sut;
        private IOutput _output;

        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _sut = new Display(_output);
        }

        [Test]
        public void Display_ShowTimeOutputsExpected()
        {
            string consoleOutput;
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);

                _sut.ShowTime(2, 10);

                consoleOutput = stringWriter.ToString();
            }

            Assert.That(consoleOutput, Is.EqualTo("Display shows: 02:10\r\n"));

        }

        [Test]
        public void ShowPowerOutputsExpected()
        {
            string consoleOutput;
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);

                _sut.ShowPower(2);

                consoleOutput = stringWriter.ToString();
            }
            Assert.That(consoleOutput, Is.EqualTo("Display shows: 2 W\r\n"));
        }

        [Test]
        public void ClearDisplaysCorrectString()
        {
            string consoleOutput;
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);

                _sut.Clear();

                consoleOutput = stringWriter.ToString();
            }
            Assert.That(consoleOutput, Is.EqualTo("Display cleared\r\n"));
        }
    }
}
