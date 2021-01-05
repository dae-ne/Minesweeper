using Caliburn.Micro;
using Minesweeper.BusinessLogic;
using Minesweeper.Test.EventModels;
using System.Threading.Tasks;

namespace Minesweeper.Test.ViewModels
{
    class MenuViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _events;
        private int _boardHeight;
        private int _boardWidth;
        private int _numberOfMines;

        public MenuViewModel(IEventAggregator events)
        {
            _events = events;
            BoardHeight = MinNumberOfRows;
            BoardWidth = MinNumberOfColumns;
            NumberOfMines = MinNumberOfMines;
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

        public int NumberOfMines
        {
            get => _numberOfMines;
            set
            {
                var max = BoardHeight * BoardWidth;
                _numberOfMines = value >= max ? max : value; 
                NotifyOfPropertyChange(() => NumberOfMines);
            }
        }

        public int MinNumberOfColumns => Settings.MinNumberOfColumns;
        public int MinNumberOfRows => Settings.MinNumberOfRows;
        public int MinNumberOfMines => Settings.MinNumberOfMines;

        public int MaxNumberOfColumns => Settings.MaxNumberOfColumns;
        public int MaxNumberOfRows => Settings.MaxNumberOfRows;
        public int MaxNumberOfMines => Settings.MaxNumberOfMines;

        public void EasyModeBtClick()
        {
            BoardHeight = Settings.LevelEasyRows;
            BoardWidth = Settings.LevelEasyColumns;
            NumberOfMines = Settings.LevelEasyMines;
        }

        public void MediumModeBtClick()
        {
            BoardHeight = Settings.LevelMediumRows;
            BoardWidth = Settings.LevelMediumColumns;
            NumberOfMines = Settings.LevelMediumMines;
        }

        public void HardModeBtClick()
        {
            BoardHeight = Settings.LevelHardRows;
            BoardWidth = Settings.LevelHardColumns;
            NumberOfMines = Settings.LevelHardMines;
        }

        public async Task StartGameBtClick()
        {
            await _events.PublishOnUIThreadAsync(new StartGameEvent
            {
                BoardHeight = this.BoardHeight,
                BoardWidth = this.BoardWidth,
                NumberOfMines = this.NumberOfMines
            });
        }
    }
}
