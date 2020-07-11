using ReactiveUI.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompellingExample.ViewModels;
using Microsoft.AspNetCore.Components;

namespace CompellingExample.Blazor.Client.Views
{
    public partial class NugetDetailsView
    {
        [Parameter] 
        public NugetDetailsViewModel NugetViewModel { get; set; }

        protected override Task OnParametersSetAsync()
        {
            ViewModel = NugetViewModel;
            return base.OnParametersSetAsync();
        }
    }
}
