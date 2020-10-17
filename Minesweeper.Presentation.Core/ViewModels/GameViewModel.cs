using Minesweeper.Presentation.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Minesweeper.Presentation.Core.ViewModels
{
    class GameViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private ObservableCollection<FieldModel> _fields = new ObservableCollection<FieldModel>();

        public IMvxAsyncCommand GoBackCommand { get; set; }

        public IMvxCommand<FieldModel> DoSomethingCommand { get; set; }

        public ObservableCollection<FieldModel> Fields
        {
            get { return _fields; }
            set { SetProperty(ref _fields, value); }
        }

        public GameViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            GoBackCommand = new MvxAsyncCommand(GoBack);
            DoSomethingCommand = new MvxCommand<FieldModel>(DoSomething);

            Fields.Add(new FieldModel { Text = "a" });
            Fields.Add(new FieldModel { Text = "b" });
            Fields.Add(new FieldModel { Text = "c" });
            Fields.Add(new FieldModel { Text = "d" });
            Fields.Add(new FieldModel { Text = "e" });
            Fields.Add(new FieldModel { Text = "f" });
            Fields.Add(new FieldModel { Text = "g" });
            Fields.Add(new FieldModel { Text = "h" });
            Fields.Add(new FieldModel { Text = "i" });
            Fields.Add(new FieldModel { Text = "j" });
            Fields.Add(new FieldModel { Text = "k" });
            Fields.Add(new FieldModel { Text = "l" });
            Fields.Add(new FieldModel { Text = "m" });
            Fields.Add(new FieldModel { Text = "n" });
            Fields.Add(new FieldModel { Text = "o" });
            Fields.Add(new FieldModel { Text = "p" });
            Fields.Add(new FieldModel { Text = "r" });
            Fields.Add(new FieldModel { Text = "s" });
            Fields.Add(new FieldModel { Text = "t" });
            Fields.Add(new FieldModel { Text = "u" });
            Fields.Add(new FieldModel { Text = "v" });
            Fields.Add(new FieldModel { Text = "w" });
            Fields.Add(new FieldModel { Text = "x" });
            Fields.Add(new FieldModel { Text = "z" });
        }

        private async Task GoBack()
        {
            await _navigationService.Close(this);
        }

        private void DoSomething(FieldModel field)
        {
            field.Text = "";
        }
    }
}
