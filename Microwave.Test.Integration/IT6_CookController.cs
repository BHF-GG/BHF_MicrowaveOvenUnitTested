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
    public class IT6_CookController
    {
        private CookController _sut;
        private IUserInterface _userInterface;
        private IOutput _output;
        private Display _display;
        private Button _fakeButton;
        private Door _fakeDoor;
        private Light _light;
        private PowerTube _powerTube;
        private ITimer _timer;

        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _fakeButton = Substitute.For<Button>();
            _fakeDoor = Substitute.For<Door>();
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _timer = Substitute.For<Timer>();
            _sut = new CookController(_timer,_display,_powerTube);
            _userInterface =
                new UserInterface(_fakeButton, _fakeButton,
                    _fakeButton, _fakeDoor, _display, _light, _sut);
        }

        [Test]
        public void OnTimeExpiredOutputsCorrect()
        {
            _sut.StartCooking(2,2);
            _timer.Expired += Raise.EventWith(EventArgs.Empty);
            Assert.That(_output.OutTextTest,Is.EqualTo(""));
        }
    }
}
