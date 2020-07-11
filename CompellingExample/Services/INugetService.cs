using System.Collections.Generic;
using Refit;
using System.Threading.Tasks;
using CompellingExample.Blazor.Shared.Models;

namespace CompellingExample.Services
{
    public interface INugetService
    {
        [Get("/nuget/{term}")]
        Task<IEnumerable<NugetPackageDto>> GetPackages(string term);
    }
}