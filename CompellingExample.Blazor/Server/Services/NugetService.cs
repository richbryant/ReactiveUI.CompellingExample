using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CompellingExample.Blazor.Shared.Models;
using LanguageExt;
using NuGet.Configuration;
using NuGet.Protocol.Core.Types;

namespace CompellingExample.Blazor.Server.Services
{
    public interface INugetService
    {
        Task<IEnumerable<NugetPackageDto>> GetNugetPackages(string term);
    }

    public class NugetService : INugetService
    {
        public async Task<IEnumerable<NugetPackageDto>> GetNugetPackages(string term) =>
            await NugetFunctions.NuGetLocalRepository()
                .GetResource()
                .Bind(x => NugetFunctions.GetMetadata(x, term))
                .Map(x => x.AsDtos());

        public async Task<IEnumerable<NugetPackageDto>> GetNugetPackagesImperative(string term)
        {
            var sources = Repository.Provider.GetCoreV3();
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

    public static class NugetFunctions
    {
        public static SourceRepository NuGetLocalRepository()
            => new SourceRepository(new PackageSource("https://api.nuget.org/v3/index.json"),
                new List<Lazy<INuGetResourceProvider>>(Repository.Provider.GetCoreV3()));

        public static IEnumerable<NugetPackageDto> AsDtos(this IEnumerable<IPackageSearchMetadata> list)
            => list.Select(x => x.AsDto());

        public static NugetPackageDto AsDto(this IPackageSearchMetadata nuget)
            => new NugetPackageDto(nuget);

        public static async Task<PackageSearchResource> GetResource(this SourceRepository repo)
            => await repo.GetResourceAsync<PackageSearchResource>().ConfigureAwait(false);

        public static async Task<IEnumerable<IPackageSearchMetadata>> GetMetadata(PackageSearchResource source, string term)
            => await source.SearchAsync(term, new SearchFilter(false), 0, 10, null, new CancellationToken());
    }
}