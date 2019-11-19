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

        [TestCase(2,10,"2:10")]
        [TestCase(-2, -10, "2:10")]
        [TestCase(1000, -1000, "2:10")]
        public void Display_ShowTimeOutputsExpected(int min, int sec, string expected)
        {
            _sut.ShowTime(min,sec);
           
        }

        [TestCase(2,"2")]
        [TestCase(-2,"-2")]
        [TestCase(1000,"Hej")]
        public void ShowPowerOutputsExpected(int power,string expected)
        {
            _sut.ShowPower(power);
            _output.OutputLine(Arg.Is<string>(str => str == expected));
        }

        [Test]
        public void ClearDisplaysCorrectString()
        {
            _sut.Clear();
            _output.OutputLine(Arg.Is<string>(str => str.Contains("Display cleared")));
        }
    }
}
