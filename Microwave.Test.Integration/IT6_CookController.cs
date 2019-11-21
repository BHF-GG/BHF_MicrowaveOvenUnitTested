using System;
using System.IO;
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
            output = new Output();
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
            string consoleOutput;
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);

                powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
                timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
                startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
                //_sut.StartCooking(2,2);
                timer.Expired += Raise.EventWith(this, EventArgs.Empty);

                consoleOutput = stringWriter.ToString();
            }

            Assert.That(consoleOutput.Contains("Light is turned off\r\n"));
        }
    }
}
