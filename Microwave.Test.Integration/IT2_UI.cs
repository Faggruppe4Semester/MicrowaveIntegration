using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    class IT2_UI
    {
        IOutput _output;
        IDisplay _display;
        IPowerTube _powerTube;
        ITimer _timer;
        ILight _light;
        CookController _cookController;
        IButton _powerButton;
        IButton _timeButton;
        IButton _startCancelButton;
        IDoor _door;
        IUserInterface _fakeUI;
        UserInterface _uut;

        [SetUp]
        public void SetUp()
        {
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>();
            _output = Substitute.For<IOutput>();
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _timer = new Timer();
            _light = new Light(_output);
            _cookController = new CookController(_timer,_display,_powerTube);

            _uut = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);

            _fakeUI = Substitute.For<IUserInterface>();
            _cookController.UI = _fakeUI;
        }

        [Test]
        public void PowerButtonEvent_DisplayShowsPower()
        {
            _powerButton.Pressed += Raise.Event();

            _output.Received(1).OutputLine("Display shows: 50 W");
        }
        
    }
}