using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwave.Test.Integration
{
    class IT8_Button
    {
        private IOutput _output;
        private Display _display;
        private PowerTube _powerTube;
        private Light _light;

        private ITimer _timer;
        private IDoor _door;

        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        
        private CookController _cookController;
        private UserInterface _userInterface;

        [SetUp]
        public void Setup()
        {

            _output = Substitute.For<IOutput>();
            _timer = Substitute.For<ITimer>();

            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _light = new Light(_output);
                        
            _door = new Door();

            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();

            _cookController = new CookController(_timer, _display, _powerTube);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
        }

        [Test]
        public void UserInterface_PowerButtonPressed_ShowPowerCalled()
        {
            //Act:
            _powerButton.Press();

            //Assert
            _output.Received().OutputLine(Arg.Is<string>(x =>
                x == "Display shows: 50 W"));
        }


        [Test]
        public void UserInterface_TimeButtonPressed_ShowTimeCalled()
        {
            //Setup
            //Change state of UserInterface to SETPOWER
            _powerButton.Press();

            //Act:
            _timeButton.Press();

            //Assert
            _output.Received().OutputLine(Arg.Is<string>(x =>
                x == "Display shows: 01:00"));

        }

        [Test]
        public void UserInterface_StartCancelButtonPressed_TurnOnCalled()
        {
            //Setup
            //Change state of UserInterface to SETTIME
            _powerButton.Press();
            _timeButton.Press();

            //Act:
            _startCancelButton.Press();

            //Assert
            _output.Received().OutputLine(Arg.Is<string>(x =>
                x == "Light is turned on"));

        }
    }
}
