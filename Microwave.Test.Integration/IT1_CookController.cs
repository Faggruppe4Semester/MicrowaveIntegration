using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using NSubstitute.Core.Arguments;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    class IT1_CookController
    {
        CookController _uut;
        IOutput _output;
        IPowerTube _powerTube;
        ITimer _timer;
        IDisplay _display;


        [SetUp]
        public void SetUp()
        {

        }
    }
}
