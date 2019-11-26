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
    class IT9_Timer
    {
        private Timer _sut;

        private IOutput _output;
        private PowerTube _powerTube;
        private Display _display;
        private Light _light;
        private CookController _cookController;
        private UserInterface _ui;
        private Button _powerButton;
        private Button _timeButton;
        private Button _startButton;
        private Door _door;

        [SetUp]
        public void Setup()
        {
            _sut = new Timer();

            _output = Substitute.For<IOutput>();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _light = new Light(_output);
            _door = new Door();
            _powerButton = new Button();
            _timeButton = new Button();
            _startButton = new Button();
            _cookController = new CookController(_sut, _display, _powerTube);

            _ui = new UserInterface(_powerButton, _timeButton, _startButton, _door, _display, _light, _cookController);

        }

        [Test]
        public void CookController_StartCooking_LogLineTimeDisplayed()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startButton.Press();

            //Assert
            _output.Received().OutputLine(Arg.Is<string>(x =>
                x == "Display shows: 01:00"));

        }

        [Test]
        public void CookController_Stop_LogLinePowerOff()
        {
            //Getting to the right state (Cookingstate)
            _powerButton.Press();
            _timeButton.Press();
            _startButton.Press();

            //Calling stop on the cookingController
            _cookController.Stop();
            
            //Assert
            _output.Received().OutputLine(Arg.Is<string>(x =>
                x == "PowerTube turned off"));
        }

        //Testing time and display at the same time
        [Test]
        public void CookController_HandleTimerTicks_TimeOutputted()
        {
            _cookController.StartCooking(50, 2);

            System.Threading.Thread.Sleep(2100);

            //Assert
            _output.Received().OutputLine(Arg.Is<string>(x =>
                x == "Display shows: 00:00"));
        }
    }
}
