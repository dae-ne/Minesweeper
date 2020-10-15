using Caliburn.Micro;
using Saper.Presentation.ViewModels;
using System.Windows;

namespace Saper.Presentation
{
    class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
