using Caliburn.Micro;
using Minesweeper.UI.EventModels;
using System.Threading;
using System.Threading.Tasks;

namespace Minesweeper.UI.ViewModels
{
    class ShellViewModel : Conductor<object>, IHandle<StartGameEvent>, IHandle<OpenMenuEvent>, IHandle<WindowSizeChangeEvent>
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
            get => _windowHeight;
            set
            {
                _windowHeight = value;
                NotifyOfPropertyChange(() => WindowHeight);
            }
        }

        public double WindowWidth
        {
            get => _windowWidth;
            set
            {
                _windowWidth = value;
                NotifyOfPropertyChange(() => WindowWidth);
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

        public async Task HandleAsync(OpenMenuEvent message, CancellationToken cancellationToken)
        {
            WindowHeight = 400.0;
            WindowWidth = 400.0;
            await ActivateItemAsync(_menuViewModel);
        }

        public Task HandleAsync(WindowSizeChangeEvent message, CancellationToken cancellationToken)
        {
            WindowHeight = message.Height;
            WindowWidth = message.Width;
            return Task.CompletedTask;
        }
    }
}
