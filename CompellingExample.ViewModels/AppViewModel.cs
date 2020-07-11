using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using CompellingExample.ViewModels.Services;
using ReactiveUI;
using Splat;

namespace CompellingExample.ViewModels
{
    public class AppViewModel : ReactiveObject
    {
        private string _searchTerm;

        private readonly INugetService _nugetService;

        private readonly ObservableAsPropertyHelper<IEnumerable<NugetDetailsViewModel>> _searchResults;

        private readonly ObservableAsPropertyHelper<bool> _isAvailable;

        public AppViewModel(INugetService nugetService = null)
        {
            if (nugetService is null) _nugetService = Locator.Current.GetService<INugetService>();

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


        private async Task<IEnumerable<NugetDetailsViewModel>> SearchNuGetPackages(
            string term, CancellationToken token)
        {
            var result = await _nugetService.GetPackages(term);
            return result.Select(x => new NugetDetailsViewModel(x));
        }


        
    }
}