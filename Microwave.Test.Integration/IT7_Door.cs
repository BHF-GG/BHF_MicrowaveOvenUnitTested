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
using NUnit.Framework.Internal;

namespace Microwave.Test.Integration
{
    [TestFixture]
    class IT7_Door
    {
        private Door _sut;

        private Output _output;
        private ITimer _timer;
        private PowerTube _powerTube;
        private Display _display;
        private Light _light;
        private CookController _cookController;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startButton;
        private UserInterface _ui;

        [SetUp]
        public void Setup()
        {
            _sut = new Door();

            _output = new Output();
            _timer = Substitute.For<Timer>();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _light = new Light(_output);
            _cookController = new CookController(_timer, _display, _powerTube);
            _powerButton = new Button();
            _timeButton = new Button();
            _startButton = new Button();

            _ui = new UserInterface(_powerButton, _timeButton, _startButton, _sut, _display, _light, _cookController);
        }

        [Test]
        public void UserInterface_DoorOpens_TurnOnLightOutput()
        {
            _sut.Open();
            Assert.That(_output.OutTextTest, Is.EqualTo("Light is turned on"));
        }


        [Test]
        public void UserInterface_DoorWasOpenIsClosed_TurnOffLightOutput()
        {
            _sut.Open();
            _sut.Close();
            Assert.That(_output.OutTextTest, Is.EqualTo("Light is turned off"));
        }
    }
}
