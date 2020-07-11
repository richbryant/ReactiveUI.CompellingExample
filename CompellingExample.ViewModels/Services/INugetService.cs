using System.Collections.Generic;
using System.Threading.Tasks;
using CompellingExample.Blazor.Shared.Models;
using Refit;

namespace CompellingExample.ViewModels.Services
{
    public interface INugetService
    {
        [Get("/nuget/{term}")]
        Task<IEnumerable<NugetPackageDto>> GetPackages(string term);
    }
}