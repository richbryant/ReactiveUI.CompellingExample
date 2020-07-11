using System;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using CompellingExample.Blazor.Client.Views;
using CompellingExample.ViewModels;
using CompellingExample.ViewModels.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Refit;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;

namespace CompellingExample.Blazor.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            
            builder.Services
                .AddBlazorise( options =>
                {
                    options.ChangeTextOnKeyPress = false;
                } )
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();

            builder.Services.UseMicrosoftDependencyResolver();
            var resolver = Locator.CurrentMutable;
            resolver.InitializeSplat();
            resolver.InitializeReactiveUI();

            Locator.CurrentMutable.Register(() => new IndexView(), typeof(IViewFor<AppViewModel>));
            Locator.CurrentMutable.Register(() => new NugetDetailsView(), typeof(IViewFor<NugetDetailsViewModel>));

            Locator.CurrentMutable.RegisterLazySingleton(() =>
                RestService.For<INugetService>("https://localhost:44394/api", new RefitSettings{ ContentSerializer = new JsonContentSerializer()}), typeof(INugetService));

            builder.Services.AddScoped<AppViewModel>();

            

            builder.RootComponents.Add<App>("app");
            var host = builder.Build();

            host.Services
                .UseBootstrapProviders()
                .UseFontAwesomeIcons();

            await host.RunAsync();
        }
    }
}
