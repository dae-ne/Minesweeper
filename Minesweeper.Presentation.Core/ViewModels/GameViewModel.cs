using Minesweeper.Presentation.Core.Models;
using MvvmCross.Binding.Extensions;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Minesweeper.Presentation.Core.ViewModels
{
    class GameViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private ObservableCollection<FieldModel> _fields;

        public ObservableCollection<FieldModel> Fields
        {
            get => _fields;
            set => SetProperty(ref _fields, value);
        }

        private double _actualWidth;

        public double ActualWidth
        {
            get => _actualWidth;
            set => SetProperty(ref _actualWidth, value);
        }

        private double _actualHeight;

        public double ActualHeight
        {
            get => _actualHeight;
            set => SetProperty(ref _actualHeight, value);
        }


        //public int PageWidth
        //{
        //    get { return _pageWidth; }
        //    set
        //    {
        //        SetProperty(ref _pageWidth, value);
        //        RaisePropertyChanged(() => ButtonWidth);
        //    }
        //}

        //public int PageHeight
        //{
        //    get { return _pageHeight; }
        //    set
        //    {
        //        SetProperty(ref _pageHeight, value);
        //        RaisePropertyChanged(() => ButtonHeight);
        //    }
        //}

        //public int ButtonWidth
        //{
        //    get { return PageWidth / 3; }
        //}

        //public int ButtonHeight
        //{
        //    get { return PageHeight / 3; }
        //}

        public IMvxAsyncCommand GoBackCommand { get; set; }

        public IMvxCommand<FieldModel> DoSomethingCommand { get; set; }

        public GameViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            GoBackCommand = new MvxAsyncCommand(GoBack);
            DoSomethingCommand = new MvxCommand<FieldModel>(DoSomething);
            CreateFields();
        }

        private async Task GoBack()
        {
            await _navigationService.Close(this);
        }

        private void DoSomething(FieldModel field)
        {
            var index = Fields.GetPosition(field);
            Fields.RemoveAt(index);
            Fields.Insert(index, new FieldModel { Text = "1", IsEnabled = false });
        }

        private void CreateFields()
        {
            Fields = new ObservableCollection<FieldModel>();

            Fields.Add(new FieldModel { Text = "" });
            Fields.Add(new FieldModel { Text = "" });
            Fields.Add(new FieldModel { Text = "" });
            Fields.Add(new FieldModel { Text = "" });
            Fields.Add(new FieldModel { Text = "" });
            Fields.Add(new FieldModel { Text = "" });
            Fields.Add(new FieldModel { Text = "" });
            Fields.Add(new FieldModel { Text = "" });
            Fields.Add(new FieldModel { Text = "" });
            Fields.Add(new FieldModel { Text = "" });
            Fields.Add(new FieldModel { Text = "" });
            Fields.Add(new FieldModel { Text = "" });
            Fields.Add(new FieldModel { Text = "" });
            Fields.Add(new FieldModel { Text = "" });
            Fields.Add(new FieldModel { Text = "" });
            Fields.Add(new FieldModel { Text = "" });
            Fields.Add(new FieldModel { Text = "" });
            Fields.Add(new FieldModel { Text = "" });
            Fields.Add(new FieldModel { Text = "" });
            Fields.Add(new FieldModel { Text = "" });
            Fields.Add(new FieldModel { Text = "" });
            Fields.Add(new FieldModel { Text = "" });
            //Fields.Add(new FieldModel { Text = "a" });
            //Fields.Add(new FieldModel { Text = "b" });
            //Fields.Add(new FieldModel { Text = "c" });
            //Fields.Add(new FieldModel { Text = "d" });
            //Fields.Add(new FieldModel { Text = "e" });
            //Fields.Add(new FieldModel { Text = "f" });
            //Fields.Add(new FieldModel { Text = "g" });
            //Fields.Add(new FieldModel { Text = "h" });
            //Fields.Add(new FieldModel { Text = "i" });
            //Fields.Add(new FieldModel { Text = "j" });
            //Fields.Add(new FieldModel { Text = "k" });
            //Fields.Add(new FieldModel { Text = "l" });
            //Fields.Add(new FieldModel { Text = "m" });
            //Fields.Add(new FieldModel { Text = "n" });
            //Fields.Add(new FieldModel { Text = "o" });
            //Fields.Add(new FieldModel { Text = "p" });
            //Fields.Add(new FieldModel { Text = "r" });
            //Fields.Add(new FieldModel { Text = "s" });
            //Fields.Add(new FieldModel { Text = "t" });
            //Fields.Add(new FieldModel { Text = "u" });
            //Fields.Add(new FieldModel { Text = "v" });
            //Fields.Add(new FieldModel { Text = "w" });
            //Fields.Add(new FieldModel { Text = "x" });
            //Fields.Add(new FieldModel { Text = "z" });
        }
    }
}
