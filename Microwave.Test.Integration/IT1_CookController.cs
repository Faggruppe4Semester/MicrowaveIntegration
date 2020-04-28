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
using System.Configuration;
using System.Threading;
using MicrowaveOvenClasses.Boundary;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

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
        IUserInterface _ui;


        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _timer = new Timer();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _ui = Substitute.For<IUserInterface>();

            _uut = new CookController(_timer, _display, _powerTube, _ui);
        }

        [TestCase(50)]
        [TestCase(350)]
        [TestCase(700)]
        public void StartCooking_CorrectPowerShownInDisplay(int power)
        {
            _uut.StartCooking(power, 50);

            _output.Received(1).OutputLine($"PowerTube works with {power/7}");
        }

        [TestCase(1100,10,0,9)]
        [TestCase(2100,70,1,8)]
        public void StartCooking_TimerTickevent_PrintsRemaningTimeToDisplay(int waitTime,int startTime, int expectedMinutes, int expectedSeconds)
        {
            _uut.StartCooking(50, startTime);
            Thread.Sleep(waitTime);

            _output.Received(1).OutputLine($"Display shows: {expectedMinutes:D2}:{expectedSeconds:D2}");
        }

        [Test]
        public void StartCooking_TimerExpired_UiReceivesDone()
        {
            _uut.StartCooking(50,2);

            Thread.Sleep(2100);

            _ui.Received(1).CookingIsDone();
        }

        [Test]
        public void StartCooking_TimerExpired_MessageToOutput()
        {
            _uut.StartCooking(50,2);

            Thread.Sleep(2100);

            _output.Received(1).OutputLine("PowerTube turned off");
        }

        [Test]
        public void Stop_PowertubeSTurnsOff()
        {
            _uut.StartCooking(50,50);

            _uut.Stop();

            _output.Received(1).OutputLine("PowerTube turned off");
        }

        [Test]
        public void Stop_TimerTurnedOff()
        {
            _uut.StartCooking(50,50);

            _uut.Stop();

            Thread.Sleep(1100);

            _output.Received(0).OutputLine("Display shows: 00:49");
        }

    }
}
