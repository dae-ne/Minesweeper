using Caliburn.Micro;
using Minesweeper.Test.EventModels;
using System.Threading;
using System.Threading.Tasks;

namespace Minesweeper.Test.ViewModels
{
    class ShellViewModel : Conductor<object>, IHandle<StartGameEvent>
    {
        private readonly IEventAggregator _events;
        private readonly GameViewModel _gameViewModel;
        private readonly MenuViewModel _menuViewModel;
        private double _windowHeight;
        private double _windowWidth;

        public ShellViewModel(IEventAggregator events, GameViewModel gameViewModel, MenuViewModel menuViewModel)
        {
            _events = events;
            _gameViewModel = gameViewModel;
            _menuViewModel = menuViewModel;
            ActivateItemAsync(_menuViewModel);
            _events.SubscribeOnPublishedThread(this);
        }

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

        public double WindowWidth
        {
            get { return _windowWidth; }
            set
            {
                _windowWidth = value;
                NotifyOfPropertyChange(() => WindowWidth);
                _events.PublishOnUIThreadAsync(
                    new WindowSizeChangedEvent { Height = WindowHeight, Width = WindowWidth });
            }
        }

        public sealed override Task ActivateItemAsync(object item,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return base.ActivateItemAsync(item, cancellationToken);
        }

        public async Task HandleAsync(StartGameEvent message, CancellationToken cancellationToken)
        {
            await ActivateItemAsync(_gameViewModel);
        }
    }
}
