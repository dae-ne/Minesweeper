using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace Minesweeper.Presentation.Core.ViewModels
{
    class GameViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public IMvxAsyncCommand GoBackCommand { get; set; }

        public GameViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            GoBackCommand = new MvxAsyncCommand(GoBack);
        }

        private async Task GoBack()
        {
            await _navigationService.Close(this);
        }
    }
}
