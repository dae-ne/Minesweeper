using Caliburn.Micro;
using Minesweeper.BusinessLogic;
using Minesweeper.Test.EventModels;
using Minesweeper.Test.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Minesweeper.Test.ViewModels
{
    class GameViewModel : Screen, IHandle<WindowSizeChangedEvent>, IHandle<StartGameEvent>
    {
        private readonly IGameBoard _gameBoard;
        private BindableCollection<FieldModel> _fields;
        private double _windowHeght;
        private double _windowWidth;

        public GameViewModel(IEventAggregator events, IGameBoard gameBoard)
        {
            _gameBoard = gameBoard;
            events.SubscribeOnPublishedThread(this);
            Fields = new BindableCollection<FieldModel>();
            _gameBoard.GenerateBoard(10, 10, 10);

            foreach (var field in _gameBoard.Board)
            {
                Fields.Add(new FieldModel(field));
            }
        }

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

        public Task HandleAsync(WindowSizeChangedEvent message, CancellationToken cancellationToken)
        {
            WindowHeight = message.Height;
            WindowWidth = message.Width;
            return Task.CompletedTask;
        }

        public Task HandleAsync(StartGameEvent message, CancellationToken cancellationToken)
        {
            message.
            return Task.CompletedTask;
        }

        public void FieldLeftClick(FieldModel field) => UpdateField(field, _gameBoard.UncoverField);

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
