using System;
using System.Diagnostics;
using System.Reactive;
using CompellingExample.Models.Models;
using ReactiveUI;

namespace CompellingExample.ViewModels
{
    public class NugetDetailsViewModel : ReactiveObject
    {
        private readonly NugetPackageDto _dto;
        private readonly Uri _defaultUrl;

        public NugetDetailsViewModel(NugetPackageDto dto)
        {
            _dto = dto;
            _defaultUrl = new Uri("https://git.io/fAlfh");
            OpenPage = ReactiveCommand.Create(() =>
            {
                Process.Start(new ProcessStartInfo(ProjectUrl.ToString())
                {
                    UseShellExecute = true
                });
            });
        }


    
        public Uri IconUrl => _dto.IconUrl ?? _defaultUrl;
        public string Description => _dto.Description;
        public Uri ProjectUrl => _dto.ProjectUrl;
        public string Title => _dto.Title;

        public ReactiveCommand<Unit, Unit> OpenPage { get; }
    }
}