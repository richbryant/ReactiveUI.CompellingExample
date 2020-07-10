using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using NuGet.Configuration;
using NuGet.Protocol.Core.Types;
using ReactiveUI;

namespace CompellingExample.ViewModels
{
    public class AppViewModel : ReactiveObject
    {
        private string _searchTerm;

        private readonly ObservableAsPropertyHelper<IEnumerable<NugetDetailsViewModel>> _searchResults;

        private readonly ObservableAsPropertyHelper<bool> _isAvailable;

        public AppViewModel()
        {
            _searchResults = this
                .WhenAnyValue(x => x.SearchTerm)
                .Throttle(TimeSpan.FromMilliseconds(800))
                .Select(term => term?.Trim())
                .DistinctUntilChanged()
                .Where(term => !string.IsNullOrWhiteSpace(term))
                .SelectMany(SearchNuGetPackages)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToProperty(this, x => x.SearchResults);

            _searchResults.ThrownExceptions.Subscribe(error => { /* Handle errors here */ });

            _isAvailable = this
                .WhenAnyValue(x => x.SearchResults)
                .Select(searchResults => searchResults != null)
                .ToProperty(this, x => x.IsAvailable);
        }

        public bool IsAvailable => _isAvailable.Value;
        public IEnumerable<NugetDetailsViewModel> SearchResults => _searchResults.Value;

        public string SearchTerm
        {
            get => _searchTerm;
            set => this.RaiseAndSetIfChanged(ref _searchTerm, value);
        }


        private static async Task<IEnumerable<NugetDetailsViewModel>> SearchNuGetPackages(
            string term, CancellationToken token)
        {
            var providers = new List<Lazy<INuGetResourceProvider>>();
            providers.AddRange(Repository.Provider.GetCoreV3());
            var packageSource = new PackageSource("https://api.nuget.org/v3/index.json");
            var source = new SourceRepository(packageSource, providers);

            var filter = new SearchFilter(false);
            var resource = await source.GetResourceAsync<PackageSearchResource>(token).ConfigureAwait(false);
            var metadata = await resource.SearchAsync(term, filter, 0, 10, null, token).ConfigureAwait(false);
            return metadata.Select(x => new NugetDetailsViewModel(x));
        }


        
    }
}