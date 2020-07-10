using System.Reflection;
using CompellingExample.Services;
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
            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
            Locator.CurrentMutable.RegisterLazySingleton(() =>
                RestService.For<INugetService>("https://localhost:44316/api"), typeof(INugetService));
        }

    }
}
