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
    public class IT5_UserInterface
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

            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _light = new Light(_output);

            _timer = Substitute.For<ITimer>();
            _door = Substitute.For<IDoor>();
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();


            _cookController = new CookController(_timer,_display,_powerTube);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door,_display,_light,_cookController);
        }

        [Test]
        public void Light_OnDoorOpenedEvent_LightTurnedOn_OK()
        {
            //Act
            _door.Opened += Raise.EventWith(EventArgs.Empty);

            //Assert
            _output.Received().OutputLine(Arg.Is<string>(x=>
                x == "Light is turned on"));
        }

        [Test]
        public void Light_OnDoorClosedEvent_LightTurnedOff_OK()
        {
            //Setup
            _door.Opened += Raise.EventWith(EventArgs.Empty);

            //Act
            _door.Closed += Raise.EventWith(EventArgs.Empty);

            //Assert
            _output.Received().OutputLine(Arg.Is<string>(x =>
                x == "Light is turned off"));
        }

        [Test]
        public void Display_OnPowerPressedEvent_DisplayShowsPower_OK()
        {
            //Act
            _powerButton.Pressed += Raise.EventWith(EventArgs.Empty);

            //Assert
            _output.Received().OutputLine(Arg.Is<string>(x =>
                x == "Display shows: 50 W"));
        }

        [Test]
        public void Display_OnTimePressedEvent_DisplayShowsTime_OK()
        {
            //Setup
            //Change state of UserInterface to SETPOWER
            _powerButton.Pressed += Raise.EventWith(EventArgs.Empty);

            //Act
            _timeButton.Pressed += Raise.EventWith(EventArgs.Empty);

            //Assert
            _output.Received().OutputLine(Arg.Is<string>(x =>
                x == "Display shows: 01:00"));
        }

        [Test]
        public void Display_CookingIsDone_DisplayShowsClear_OK()
        {
            //Setup
            //The following sequence of events changes the state of UserInterface to COOKING
            _powerButton.Pressed += Raise.EventWith(EventArgs.Empty);
            _timeButton.Pressed += Raise.EventWith(EventArgs.Empty);
            _startCancelButton.Pressed += Raise.EventWith(EventArgs.Empty);
            
            //Act
            _userInterface.CookingIsDone();

            //Assert
            _output.Received().OutputLine(Arg.Is<string>(x =>
                x == "Display cleared"));
        }

        [Test]
        public void CookController_OnStartCancelPressedEvent_OutputShowsClear_OK()
        {
            //Setup
            //The following sequence of events changes the state of UserInterface to SETTIME
            _powerButton.Pressed += Raise.EventWith(EventArgs.Empty);
            _timeButton.Pressed += Raise.EventWith(EventArgs.Empty);


            //Act
            _startCancelButton.Pressed += Raise.EventWith(EventArgs.Empty);

            //Assert
            _output.Received().OutputLine(Arg.Is<string>(x =>
                x == "PowerTube works with 50 %"));
        }

        [Test]
        public void CookController_OnDoorOpenedEvent_OutputShowsTurnedOff_OK()
        {
            //Setup
            //The following sequence of events changes the state of UserInterface to COOKING
            _powerButton.Pressed += Raise.EventWith(EventArgs.Empty);
            _timeButton.Pressed += Raise.EventWith(EventArgs.Empty);
            _startCancelButton.Pressed += Raise.EventWith(EventArgs.Empty);

            //Act
            _door.Opened += Raise.EventWith(EventArgs.Empty);

            //Assert
            _output.Received().OutputLine(Arg.Is<string>(x =>
                x == "PowerTube turned off"));
        }


    }

}
