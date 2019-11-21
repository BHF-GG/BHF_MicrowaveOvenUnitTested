using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT4a_CookController
    {
        private CookController _sut;

        private IOutput _output;

        private Display _display;
        private PowerTube _powerTube;
        private Timer _timer;

        [SetUp]
        public void Setup()
        {
            _timer = new Timer();
            _output = Substitute.For<IOutput>();

            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _sut = new CookController(_timer,_display,_powerTube);
        }

        //Testing time and display at the same time
        [Test]
        public void Timer_TimerTickEventRaised_EventRaised()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            _timer.TimerTick += (sender, args) => pause.Set();
            _sut.StartCooking(50, 5);

            // wait for a tick, but no longer
            Assert.That(pause.WaitOne(1100));
        }
    }
}
