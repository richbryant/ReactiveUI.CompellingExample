using System;
using NuGet.Protocol.Core.Types;

namespace CompellingExample.Shared.Models
{
    public class NugetPackageDto
    {
        private readonly Uri _defaultUrl;

        public NugetPackageDto()
        { }

        public NugetPackageDto(IPackageSearchMetadata metadata)
        {
            _defaultUrl = new Uri("https://git.io/fAlfh");
            IconUrl = metadata.IconUrl ?? _defaultUrl;
            Description = metadata.Description;
            ProjectUrl = metadata.ProjectUrl;
            Title = metadata.Title;
        }
    
        public Uri IconUrl { get; set; } 
        public string Description { get; set; }
        public Uri ProjectUrl { get; set; }
        public string Title { get; set; }
    }
}