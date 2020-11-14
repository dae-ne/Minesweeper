using Caliburn.Micro;
using Minesweeper.BusinessLogic;
using Minesweeper.Test.EventModels;
using Minesweeper.Test.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Minesweeper.Test.ViewModels
{
    class GameViewModel : Screen, IHandle<WindowSizeChangedEvent>, IHandle<StartGameEvent>
    {
        private BindableCollection<FieldModel> _fields;

        public BindableCollection<FieldModel> Fields
        {
            get { return _fields; }
            set
            {
                _fields = value;
                NotifyOfPropertyChange(() => Fields);
            }
        }

        public double WindowHeight
        {
            get { return _windowHeght; }
            set { _windowHeght = value; }
        }

        public double WindowWidth
        {
            get { return _windowWidth; }
            set { _windowWidth = value; }
        }

        private double _windowHeght;
        private double _windowWidth;
        private readonly IGameBoard _gameBoard;

        public GameViewModel(IEventAggregator events, IGameBoard gameBoard)
        {
            _gameBoard = gameBoard;
            events.SubscribeOnPublishedThread(this);
            Fields = new BindableCollection<FieldModel>();

            _gameBoard.GenerateBoard(50, 50, 2);

            foreach (var field in _gameBoard.Board)
            {
                Fields.Add(new FieldModel { Text = field.Value.ToString(), IsEnabled = true });
            }
        }

        public Task HandleAsync(WindowSizeChangedEvent message, CancellationToken cancellationToken)
        {
            WindowHeight = message.Height;
            WindowWidth = message.Width;
            return Task.CompletedTask;
        }

        public Task HandleAsync(StartGameEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void DoSomething(FieldModel field)
        {
            var index = Fields.IndexOf(field);
            Fields.RemoveAt(index);
            Fields.Insert(index, new FieldModel { Text = "1", IsEnabled = false });
        }
    }
}
