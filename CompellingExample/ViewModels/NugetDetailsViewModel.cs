using System;
using System.Diagnostics;
using System.Reactive;
using NuGet.Protocol.Core.Types;
using ReactiveUI;

namespace CompellingExample.ViewModels
{
    public class NugetDetailsViewModel : ReactiveObject
    {
        private readonly IPackageSearchMetadata _metadata;
        private readonly Uri _defaultUrl;

        public NugetDetailsViewModel(IPackageSearchMetadata metadata)
        {
            _metadata = metadata;
            _defaultUrl = new Uri("https://git.io/fAlfh");
            OpenPage = ReactiveCommand.Create(() =>
            {
                Process.Start(new ProcessStartInfo(ProjectUrl.ToString())
                {
                    UseShellExecute = true
                });
            });
        }
    
        public Uri IconUrl => _metadata.IconUrl ?? _defaultUrl;
        public string Description => _metadata.Description;
        public Uri ProjectUrl => _metadata.ProjectUrl;
        public string Title => _metadata.Title;

        public ReactiveCommand<Unit, Unit> OpenPage { get; }
    }
}