using Caliburn.Micro;
using Minesweeper.Test.EventModels;
using System.Threading;
using System.Threading.Tasks;

namespace Minesweeper.Test.ViewModels
{
    class ShellViewModel : Conductor<object>, IHandle<StartGameEvent>
    {
        private readonly IEventAggregator _events;
        private readonly GameViewModel _gameVM;
        private readonly MenuViewModel _menuVM;

        private double _windowHeight;

        public double WindowHeight
        {
            get { return _windowHeight; }
            set
            {
                _windowHeight = value;
                NotifyOfPropertyChange(() => WindowHeight);
                _events.PublishOnUIThreadAsync(
                    new WindowSizeChangedEvent { Height = WindowHeight, Width = WindowWidth });
            }
        }

        private double _windowWidth;

        public double WindowWidth
        {
            get { return _windowWidth; }
            set
            {
                _windowWidth = value;
                NotifyOfPropertyChange(() => WindowWidth);
                _events.PublishOnUIThreadAsync(
                    new WindowSizeChangedEvent{ Height = WindowHeight, Width = WindowWidth });
            }
        }

        public ShellViewModel(IEventAggregator events, GameViewModel gameVM, MenuViewModel menuVM)
        {
            _events = events;
            _gameVM = gameVM;
            _menuVM = menuVM;
            ActivateItemAsync(_menuVM);
            _events.SubscribeOnPublishedThread(this);
        }

        public async Task HandleAsync(StartGameEvent message, CancellationToken cancellationToken)
        {
            await ActivateItemAsync(_gameVM);
        }
    }
}
