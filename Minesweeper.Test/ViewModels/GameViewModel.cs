using Caliburn.Micro;
using Minesweeper.BusinessLogic;
using Minesweeper.Test.EventModels;
using Minesweeper.Test.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Minesweeper.Test.ViewModels
{
    class GameViewModel : Screen, IHandle<WindowSizeChangedEvent>, IHandle<StartGameEvent>
    {
        private readonly IEventAggregator _events;
        private readonly IGameBoard _gameBoard;
        private readonly IBoardScanner _scanner;
        private BindableCollection<FieldModel> _fields;
        private double _windowHeight;
        private double _windowWidth;
        private double _boardHeight;
        private double _boardWidth;
        private int _boardRows;
        private int _boardColumns;
        private int _numberOfMines;
        private Stopwatch _stopwatch;
        private bool _gameStarted = false;
        private bool _gameFinished = false;

        public GameViewModel(IEventAggregator events, IGameBoard gameBoard, IBoardScanner scanner)
        {
            _events = events;
            _gameBoard = gameBoard;
            _scanner = scanner;
            events.SubscribeOnPublishedThread(this);
            Fields = new BindableCollection<FieldModel>();
            _stopwatch = new Stopwatch();
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
            get => _windowHeight;
            set { _windowHeight = value; }
        }

        public double WindowWidth
        {
            get => _windowWidth;
            set { _windowWidth = value; }
        }

        public double BoardHeight
        {
            get => _boardHeight;
            set
            {
                _boardHeight = value;
                NotifyOfPropertyChange(() => Fields);
            }
        }

        public double BoardWidth
        {
            get => _boardWidth;
            set
            {
                _boardWidth = value;
                NotifyOfPropertyChange(() => Fields);
            }
        }

        public int BoardRows
        {
            get => _boardRows;
            set
            {
                _boardRows = value;
                NotifyOfPropertyChange(() => BoardRows);
            }
        }

        public int BoardColumns
        {
            get => _boardColumns;
            set
            {
                _boardColumns = value;
                NotifyOfPropertyChange(() => BoardColumns);
            }
        }

        public string GameScore
        {
            get => GetStatsStringValue(_gameBoard.Score);
        }

        public string InGameTime
        {
            get => GetStatsStringValue((int)_stopwatch.Elapsed.TotalSeconds);
        }

        public async Task StartOverMenuClick()
        {
            await _events.PublishOnUIThreadAsync(new StartGameEvent
            {
                BoardHeight = this.BoardRows,
                BoardWidth = this.BoardColumns,
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
            ResetGame();
            BoardRows = message.BoardHeight;
            BoardColumns = message.BoardWidth;
            _numberOfMines = message.NumberOfMines;
            _gameBoard.GenerateBoard(BoardColumns, BoardRows, _numberOfMines);
            NotifyOfPropertyChange(() => GameScore);
            Fields.Clear();

            foreach (var field in _gameBoard.Board)
            {
                Fields.Add(new FieldModel(field));
            }

            return Task.CompletedTask;
        }

        public void FieldLeftClick(FieldModel field)
        {
            _ = StartGame();
            UpdateField(field, _gameBoard.UncoverField);

            switch (field.Value)
            {
                case FieldValues.Empty:
                    var adjecentEmptyFields = _scanner.FindAdjacentEmpty(_gameBoard, field);

                    foreach (var emptyField in adjecentEmptyFields)
                    {
                        var fieldModel = _fields
                            .Where(e => e.Id == emptyField.Id)
                            .FirstOrDefault();
                        UpdateField(fieldModel, _gameBoard.UncoverField);
                    }

                    break;

                case FieldValues.Mine:
                    EndGame(false);
                    break;
            }
        }

        public void FieldRightClick(FieldModel field)
        {
            _ = StartGame();
            UpdateField(field, _gameBoard.SetNextStatus);
            NotifyOfPropertyChange(() => GameScore);
        }

        private void UpdateField(FieldModel field, Action<IModel> action)
        {
            var index = Fields.IndexOf(field);
            action(field);
            Fields.RemoveAt(index);
            Fields.Insert(index, field);
        }

        private async Task StartGame()
        {
            if (!_gameStarted)
            {
                _gameStarted = true;
                _stopwatch.Restart();

                while (!_gameFinished)
                {
                    await Task.Delay(1000);
                    NotifyOfPropertyChange(() => InGameTime);
                }
            }
        }

        private void EndGame(bool win)
        {
            if (!_gameFinished)
            {
                _gameFinished = true;
                _stopwatch.Stop();

                var message = win ? $"Congratulations, your time: {InGameTime} s" : "Boooooooom!";
                MessageBox.Show(message);
            }
        }

        private void ResetGame()
        {
            _gameStarted = false;
            _gameFinished = false;

            _stopwatch.Restart();
            _stopwatch.Stop();

            NotifyOfPropertyChange(() => InGameTime);
        }

        private string GetStatsStringValue(int value)
        {
            var valueAsString = value.ToString();

            for (var i = 0; i < 3; i++)
            {
                if (valueAsString.Length < 3)
                {
                    valueAsString = "0" + valueAsString;
                }
            }

            return valueAsString;
        }
    }
}
