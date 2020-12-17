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
        private readonly IGameBoard _gameBoard;
        private readonly IBoardScanner _scanner;
        private BindableCollection<FieldModel> _fields;
        private double _windowHeght;
        private double _windowWidth;
        private int _boardHeight;
        private int _boardWidth;

        public GameViewModel(IEventAggregator events, IGameBoard gameBoard, IBoardScanner scanner)
        {
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
            _gameBoard.GenerateBoard(BoardWidth, BoardHeight, message.NumberOfMines);

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
