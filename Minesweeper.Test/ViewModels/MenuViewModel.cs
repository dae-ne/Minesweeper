using Caliburn.Micro;
using Minesweeper.Test.EventModels;
using System.Threading.Tasks;

namespace Minesweeper.Test.ViewModels
{
    class MenuViewModel
    {
        private readonly IEventAggregator _events;

        public MenuViewModel(IEventAggregator events)
        {
            _events = events;
        }

        public async Task StartGameBtClick()
        {
            await _events.PublishOnUIThreadAsync(new StartGameEvent());
        }
    }
}
