using Caliburn.Micro;
using Minesweeper.GameLogic;
using Minesweeper.UI.EventModels;
using System.Threading.Tasks;

namespace Minesweeper.UI.ViewModels
{
    class MenuViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _events;
        private int _boardRows;
        private int _boardColumns;
        private int _numberOfMines;

        public MenuViewModel(IEventAggregator events)
        {
            _events = events;
            BoardRows = MinNumberOfRows;
            BoardColumns = MinNumberOfColumns;
            NumberOfMines = MinNumberOfMines;
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

        public int NumberOfMines
        {
            get => _numberOfMines;
            set
            {
                var max = BoardRows * BoardColumns;
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
            BoardRows = Settings.LevelEasyRows;
            BoardColumns = Settings.LevelEasyColumns;
            NumberOfMines = Settings.LevelEasyMines;
        }

        public void MediumModeBtClick()
        {
            BoardRows = Settings.LevelMediumRows;
            BoardColumns = Settings.LevelMediumColumns;
            NumberOfMines = Settings.LevelMediumMines;
        }

        public void HardModeBtClick()
        {
            BoardRows = Settings.LevelHardRows;
            BoardColumns = Settings.LevelHardColumns;
            NumberOfMines = Settings.LevelHardMines;
        }

        public async Task StartGameBtClick()
        {
            await _events.PublishOnUIThreadAsync(new StartGameEvent
            {
                BoardHeight = this.BoardRows,
                BoardWidth = this.BoardColumns,
                NumberOfMines = this.NumberOfMines
            });
        }
    }
}
