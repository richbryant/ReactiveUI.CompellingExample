using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CompellingExample.Models.Models;
using NuGet.Configuration;
using NuGet.Protocol.Core.Types;

namespace CompellingExample.WebApi.Services
{
    public interface INugetService
    {
        Task<IEnumerable<NugetPackageDto>> GetNugetPackages(string term);
    }

    public class NugetService : INugetService
    {
        public async Task<IEnumerable<NugetPackageDto>> GetNugetPackages(string term)
        {
            var providers = new List<Lazy<INuGetResourceProvider>>();
            providers.AddRange(Repository.Provider.GetCoreV3());
            var packageSource = new PackageSource("https://api.nuget.org/v3/index.json");
            var source = new SourceRepository(packageSource, providers);

            var filter = new SearchFilter(false);
            var resource = await source.GetResourceAsync<PackageSearchResource>().ConfigureAwait(false);
            var metadata = await resource.SearchAsync(term, filter, 0, 10, null, new CancellationToken())
                .ConfigureAwait(false);
            return metadata.Select(x => new NugetPackageDto(x));
        }
    }
}