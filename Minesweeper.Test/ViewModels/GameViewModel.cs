using Caliburn.Micro;
using Minesweeper.BusinessLogic;
using Minesweeper.Test.EventModels;
using Minesweeper.Test.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Minesweeper.Test.ViewModels
{
    class GameViewModel : Screen, IHandle<WindowSizeChangedEvent>, IHandle<StartGameEvent>
    {
        private readonly IEventAggregator _events;
        private readonly IGameBoard _gameBoard;
        private readonly IBoardScanner _scanner;
        private BindableCollection<FieldModel> _fields;
        private double _windowHeght;
        private double _windowWidth;
        private int _boardHeight;
        private int _boardWidth;
        private int _numberOfMines;

        public GameViewModel(IEventAggregator events, IGameBoard gameBoard, IBoardScanner scanner)
        {
            _events = events;
            _gameBoard = gameBoard;
            _scanner = scanner;
            events.SubscribeOnPublishedThread(this);
            Fields = new BindableCollection<FieldModel>();
        }

        public BindableCollection<FieldModel> Fields
        {
            get => _fields;
            set
            {
                _fields = value;
                NotifyOfPropertyChange(() => Fields);
            }
        }

        public double WindowHeight
        {
            get => _windowHeght;
            set { _windowHeght = value; }
        }

        public double WindowWidth
        {
            get => _windowWidth;
            set { _windowWidth = value; }
        }

        public int BoardHeight
        {
            get => _boardHeight;
            set
            {
                _boardHeight = value;
                NotifyOfPropertyChange(() => BoardHeight);
            }
        }

        public int BoardWidth
        {
            get => _boardWidth;
            set
            {
                _boardWidth = value;
                NotifyOfPropertyChange(() => BoardWidth);
            }
        }

        public async Task StartOverMenuClick()
        {
            await _events.PublishOnUIThreadAsync(new StartGameEvent
            {
                BoardHeight = this.BoardHeight,
                BoardWidth = this.BoardWidth,
                NumberOfMines = _numberOfMines
            });
        }

        public async Task NewGameEasyMenuClick()
        {
            await _events.PublishOnUIThreadAsync(new StartGameEvent
            {
                BoardHeight = Settings.LevelEasyRows,
                BoardWidth = Settings.LevelEasyColumns,
                NumberOfMines = Settings.LevelEasyMines
            });
        }

        public async Task NewGameMediumMenuClick()
        {
            await _events.PublishOnUIThreadAsync(new StartGameEvent
            {
                BoardHeight = Settings.LevelMediumRows,
                BoardWidth = Settings.LevelMediumColumns,
                NumberOfMines = Settings.LevelMediumMines
            });
        }

        public async Task NewGameHardMenuClick()
        {
            await _events.PublishOnUIThreadAsync(new StartGameEvent
            {
                BoardHeight = Settings.LevelHardRows,
                BoardWidth = Settings.LevelHardColumns,
                NumberOfMines = Settings.LevelHardMines
            });
        }

        public async Task OpenMenuMenuClick()
        {
            await _events.PublishOnUIThreadAsync(new OpenMenuEvent());
        }

        public void QuitMenuClick()
        {
            System.Windows.Application.Current.Shutdown();
        }

        public Task HandleAsync(WindowSizeChangedEvent message, CancellationToken cancellationToken)
        {
            WindowHeight = message.Height;
            WindowWidth = message.Width;
            return Task.CompletedTask;
        }

        public Task HandleAsync(StartGameEvent message, CancellationToken cancellationToken)
        {
            BoardHeight = message.BoardHeight;
            BoardWidth = message.BoardWidth;
            _numberOfMines = message.NumberOfMines;
            _gameBoard.GenerateBoard(BoardWidth, BoardHeight, _numberOfMines);
            Fields.Clear();

            foreach (var field in _gameBoard.Board)
            {
                Fields.Add(new FieldModel(field));
            }

            return Task.CompletedTask;
        }

        public void FieldLeftClick(FieldModel field)
        {
            UpdateField(field, _gameBoard.UncoverField);

            if (field.LogicModel.Value == FieldValues.Empty)
            {
                var adjecentEmptyFields = _scanner.FindAdjacentEmpty(_gameBoard, field.LogicModel);

                foreach (var emptyField in adjecentEmptyFields)
                {
                    var fieldModel = _fields
                        .Where(e => e.LogicModel == emptyField)
                        .FirstOrDefault();
                    UpdateField(fieldModel, _gameBoard.UncoverField);
                }
            }
        }

        public void FieldRightClick(FieldModel field) => UpdateField(field, _gameBoard.SetNextStatus);

        private void UpdateField(FieldModel field, Action<Model> action)
        {
            var index = Fields.IndexOf(field);
            action(field.LogicModel);
            Fields.RemoveAt(index);
            Fields.Insert(index, field);
        }
    }
}
