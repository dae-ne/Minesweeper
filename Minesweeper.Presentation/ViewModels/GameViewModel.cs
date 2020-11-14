using Caliburn.Micro;
using Minesweeper.Presentation.Models;

namespace Minesweeper.Presentation.ViewModels
{
    class GameViewModel : Conductor<object>
    {
        private const string MenuButtonText = "Start Game";
        private const string GameButtonText = "Stop";
        private readonly GameStatusViewModel _gameStatusViewModel;
        private readonly MenuViewModel _menuViewModel;
        private BindableCollection<FieldModel> _fields;

        public BindableCollection<FieldModel> Fields
        {
            get { return _fields; }
            set
            {
                _fields = value;
                NotifyOfPropertyChange(() => Fields);
            }
        }

        private double _windowWidth;

        public double WindowWidth
        {
            get { return _windowWidth; }
            set
            {
                _windowWidth = value;
                NotifyOfPropertyChange(() => WindowWidth);
                NotifyOfPropertyChange(() => FieldWidth);
            }
        }

        private double _windowHeight;

        public double WindowHeight
        {
            get { return _windowHeight; }
            set
            {
                _windowHeight = value;
                NotifyOfPropertyChange(() => WindowHeight);
                NotifyOfPropertyChange(() => FieldHeight);
            }
        }

        private string _navButtonText;

        public string NavButtonText
        {
            get { return _navButtonText; }
            set
            {
                _navButtonText = value;
                NotifyOfPropertyChange(() => NavButtonText);
            }
        }

        public double FieldWidth
        {
            get { return (WindowWidth - 215.0) / 12.0; }
        }

        public double FieldHeight
        {
            get { return WindowHeight / 8.0; }
        }

        public GameViewModel(GameStatusViewModel gameStatusViewModel, MenuViewModel menuViewModel)
        {
            NavButtonText = MenuButtonText;

            Fields = new BindableCollection<FieldModel>();
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "aside", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "", IsEnabled = true });
            _gameStatusViewModel = gameStatusViewModel;
            _menuViewModel = menuViewModel;
            ActivateItem(_menuViewModel);
        }

        public void NavigateClick()
        {
            if (NavButtonText == MenuButtonText)
            {
                NavButtonText = GameButtonText;
                ActivateItem(_gameStatusViewModel);
            }
            else
            {
                NavButtonText = MenuButtonText;
                ActivateItem(_menuViewModel);
            }
        }

        public void DoSomething(FieldModel field)
        {
            var index = Fields.IndexOf(field);
            Fields.RemoveAt(index);
            Fields.Insert(index, new FieldModel { Text = "1", IsEnabled = false });
        }
    }
}
