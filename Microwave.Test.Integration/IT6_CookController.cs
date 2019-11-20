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
    public class IT6_CookController
    {
        private CookController _sut;
        private IUserInterface _userInterface;
        private IOutput _output;
        private Display _display;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IDoor _fakeDoor;
        private Light _light;
        private PowerTube _powerTube;
        private ITimer _timer;

        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
            _fakeDoor = Substitute.For<IDoor>();
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _timer = Substitute.For<ITimer>();
            _sut = new CookController(_timer,_display,_powerTube);
            _userInterface =
                new UserInterface(_powerButton, _timeButton,
                    _startCancelButton, _fakeDoor, _display, _light, _sut);
        }

        [Test]
        public void OnTimeExpiredOutputsCorrect()
        {
            _powerButton.Pressed += Raise.EventWith(this,EventArgs.Empty);
            _timeButton.Pressed += Raise.EventWith(this,EventArgs.Empty);
            _startCancelButton.Pressed += Raise.EventWith(this,EventArgs.Empty);
            _sut.StartCooking(2,2);
            _timer.Expired += Raise.EventWith(this,EventArgs.Empty);
            _output.Received().OutputLine(Arg.Is<string>(x=> x == "Light is turned off"));
        }
    }
}
