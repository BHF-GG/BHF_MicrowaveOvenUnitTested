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
    public class IT4_CookController
    {
        //private EventArgs _receivedEventArgs;

        private CookController _sut;

        private IOutput _output;

        private Display _display;
        private PowerTube _powerTube;
        private ITimer _timer;


        //private IButton _powerButton;
        //private IButton _timeButton;
        //private IButton _startCancelButton;
        //private UserInterface _userInterface;

        [SetUp]
        public void Setup()
        {
            _timer = Substitute.For<ITimer>();
            _output = Substitute.For<IOutput>();

            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _sut = new CookController(_timer,_display,_powerTube);

            //_powerButton = Substitute.For<IButton>();
            //_timeButton = Substitute.For<IButton>();
            //_startCancelButton = Substitute.For<IButton>();
            //_userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);

            //Testing timerTickEvents
            //_receivedEventArgs = null;

            //Fake Event Handler
            //_timer.TimerTick += (o, args) =>
            //{
            //    _receivedEventArgs = args;
            //};
        }

        //Testing time and display at the same time
        [Test]
        public void TimerPlusDisplay_HandleTimerTick_TimeOutputted()
        {
            //_timer.Start(50);

            _timer.TimerTick += Raise.EventWith(EventArgs.Empty);

            //Assert
            _output.Received().OutputLine(Arg.Is<string>(x =>
                x == "Display shows: 00:00"));
        }

        [Test]
        public void PowerTube_StartCooking_PowerOutputted()
        {
            _sut.StartCooking(50, 0);

            //Assert
            _output.Received().OutputLine(Arg.Is<string>(x =>
                x == "PowerTube works with 50 %"));
        }
    }
}
