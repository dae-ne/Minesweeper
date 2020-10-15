using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace Minesweeper.Presentation.Core.ViewModels
{
    class MenuViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public IMvxAsyncCommand NewGameEasyCommand { get; set; }

        public IMvxAsyncCommand NewGameNormalCommand { get; set; }

        public IMvxAsyncCommand NewGameHardCommand { get; set; }

        public MenuViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            NewGameEasyCommand = new MvxAsyncCommand(NewGameEasy);
            NewGameNormalCommand = new MvxAsyncCommand(NewGameNormal);
            NewGameHardCommand = new MvxAsyncCommand(NewGameHard);
        }

        private async Task NewGameEasy()
        {
            await _navigationService.Navigate<GameViewModel>();
        }

        private async Task NewGameNormal()
        {
            await _navigationService.Navigate<GameViewModel>();
        }

        private async Task NewGameHard()
        {
            await _navigationService.Navigate<GameViewModel>();
        }
    }
}
