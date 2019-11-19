using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Microwave.Test.Integration
{
    [TestFixture]
    class IT4_CookController
    {
        private Output _output;
        private Display _display;
        private PowerTube _powerTube;
        private ITimer _timer;

        [SetUp]
        public void Setup()
        {
            _timer = Substitute.For<ITimer>();
            _output = new Output();
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);

        }
    }
}
