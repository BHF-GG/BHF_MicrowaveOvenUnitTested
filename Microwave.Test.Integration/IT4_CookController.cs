using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT4_CookController
    {
        private CookController _sut;

        private IOutput _output;

        private Display _display;
        private PowerTube _powerTube;
        private ITimer _timer;

        [SetUp]
        public void Setup()
        {
            _timer = Substitute.For<ITimer>();
            _output = Substitute.For<IOutput>();

            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _sut = new CookController(_timer, _display, _powerTube);
        }

        [Test]
        public void PowerTube_StartCooking_PowerOutputted()
        {
            _sut.StartCooking(350, 0);

            //Assert
            _output.Received().OutputLine(Arg.Is<string>(x =>
                x == "PowerTube works with 50 %"));
        }

        [Test]
        public void PowerTube_CookingStatedStoppedPowerTurnedOff_PowerOffOutputted()
        {
            _sut.StartCooking(50, 5);
            _sut.Stop();

            //Assert
            _output.Received().OutputLine(Arg.Is<string>(x =>
                x == "PowerTube turned off"));
        }

    }
}
