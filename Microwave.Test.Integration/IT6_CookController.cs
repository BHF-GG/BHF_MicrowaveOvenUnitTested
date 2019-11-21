using System;
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
        private CookController sut;
        private IUserInterface userInterface;
        private IOutput output;
        private IDisplay display;
        private IButton powerButton;
        private IButton timeButton;
        private IButton startCancelButton;
        private IDoor fakeDoor;
        private ILight light;
        private IPowerTube powerTube;
        private ITimer timer;

        [SetUp]
        public void SetUp()
        {
            output = Substitute.For<IOutput>();
            powerButton = Substitute.For<IButton>();
            timeButton = Substitute.For<IButton>();
            startCancelButton = Substitute.For<IButton>();
            fakeDoor = Substitute.For<IDoor>();
            timer = Substitute.For<ITimer>();


            display = new Display(output);
            powerTube = new PowerTube(output);
            light = new Light(output);

            //sut = new CookController(timer, display, powerTube);
            userInterface =
                new UserInterface(powerButton, timeButton,
                    startCancelButton, fakeDoor, display, light, new CookController(timer,display,powerTube,userInterface));

        }

        [Test]
        public void OnTimeExp()
        {
            timer.Expired += Raise.EventWith(this, EventArgs.Empty);
        }

        [Test]
        public void OnTimeExpiredOutputsCorrect()
        {
            powerButton.Pressed += Raise.EventWith(this,EventArgs.Empty);
            timeButton.Pressed += Raise.EventWith(this,EventArgs.Empty);
            startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            //_sut.StartCooking(2,2);
            timer.Expired += Raise.EventWith(this, EventArgs.Empty);
            output.Received().OutputLine(Arg.Is<string>(x=> x == "Light is turned off"));
        }
    }
}
