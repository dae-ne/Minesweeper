using Caliburn.Micro;
using Minesweeper.Test.EventModels;
using Minesweeper.Test.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Minesweeper.Test.ViewModels
{
    class GameViewModel : Screen, IHandle<WindowSizeChangedEvent>, IHandle<StartGameEvent>
    {
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

        private double _windowHeght;

        public double WindowHeight
        {
            get { return _windowHeght; }
            set { _windowHeght = value; }
        }

        private double _windowWidth;

        public double WindowWidth
        {
            get { return _windowWidth; }
            set { _windowWidth = value; }
        }

        public GameViewModel(IEventAggregator events)
        {
            events.SubscribeOnPublishedThread(this);

            Fields = new BindableCollection<FieldModel>();
            Fields.Add(new FieldModel { Text = "a", IsEnabled = true });
            Fields.Add(new FieldModel { Text = "b", IsEnabled = true });
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
        }

        public Task HandleAsync(WindowSizeChangedEvent message, CancellationToken cancellationToken)
        {
            WindowHeight = message.Height;
            WindowWidth = message.Width;
            return Task.CompletedTask;
        }

        public Task HandleAsync(StartGameEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void DoSomething(FieldModel field)
        {
            var index = Fields.IndexOf(field);
            Fields.RemoveAt(index);
            Fields.Insert(index, new FieldModel { Text = "1", IsEnabled = false });
        }
    }
}
