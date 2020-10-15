using MvvmCross.ViewModels;
using Saper.Presentation.Core.ViewModels;

namespace Saper.Presentation.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<MenuViewModel>();
        }
    }
}
