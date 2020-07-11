using System;
using NuGet.Protocol.Core.Types;

namespace CompellingExample.Blazor.Shared.Models
{
    public class NugetPackageDto
    {
        public NugetPackageDto()
        { }

        public NugetPackageDto(IPackageSearchMetadata metadata)
        {
            IconUrl = metadata.IconUrl ?? new Uri("https://git.io/fAlfh");
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