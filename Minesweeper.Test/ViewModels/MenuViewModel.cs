using Caliburn.Micro;
using Minesweeper.Test.EventModels;
using System.Threading.Tasks;

namespace Minesweeper.Test.ViewModels
{
    class MenuViewModel : Screen
    {
        private readonly IEventAggregator _events;
        private int _boardHeight;
        private int _boardWidth;
        private int _numberOfMines;

        public MenuViewModel(IEventAggregator events)
        {
            _events = events;
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
                _numberOfMines = value;
                NotifyOfPropertyChange(() => NumberOfMines);
            }
        }

        public void EasyModeBtClick()
        {
            BoardHeight = 3;
            BoardWidth = 3;
            NumberOfMines = 3;
        }

        public void MediumModeBtClick()
        {
            BoardHeight = 6;
            BoardWidth = 6;
            NumberOfMines = 6;
        }

        public void HardModeBtClick()
        {
            BoardHeight = 9;
            BoardWidth = 9;
            NumberOfMines = 9;
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
