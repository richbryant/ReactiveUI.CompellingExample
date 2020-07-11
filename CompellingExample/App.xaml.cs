using CompellingExample.ViewModels;
using CompellingExample.ViewModels.Services;
using CompellingExample.Views;
using ReactiveUI;
using Refit;
using Splat;

namespace CompellingExample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            Locator.CurrentMutable.Register(() => new MainWindow(), typeof(IViewFor<AppViewModel>));
            Locator.CurrentMutable.Register(() => new NugetDetailsView(), typeof(IViewFor<NugetDetailsViewModel>));
            
            Locator.CurrentMutable.RegisterLazySingleton(() =>
                RestService.For<INugetService>("https://localhost:44394/api"), typeof(INugetService));
            
        }

    }
}
