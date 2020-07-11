using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using Blazorise;
using CompellingExample.ViewModels;
using Microsoft.AspNetCore.Components;
using ReactiveUI;
using ReactiveUI.Blazor;
using Splat;

namespace CompellingExample.Blazor.Client.Views
{
    public partial class IndexView 
    {
        private bool _showResults;
        public bool ShowResults
        {
            get => _showResults;
            set
            {
                _showResults = value;
                StateHasChanged();
            } 
        }

        public IndexView()
        {
            ViewModel = new AppViewModel();

            this.WhenActivated(disposableRegistration =>
            {
                this.OneWayBind(ViewModel, 
                        viewModel => viewModel.IsAvailable, 
                        view => view.ShowResults)
                    .DisposeWith(disposableRegistration); 
                
            });
        }

        private void SearchTextChanged()
        {
            Console.WriteLine(ViewModel.SearchTerm);
            Console.WriteLine($"SearchResults is {ViewModel.SearchResults.Count()}");
        }
    }
}
