using System.Threading.Tasks;
using CompellingExample.Blazor.Server.Extensions;
using CompellingExample.Blazor.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompellingExample.Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NugetController : ControllerBase
    {
        private readonly INugetService _nugetService;

        public NugetController(INugetService nugetService) => _nugetService = nugetService;


        [HttpGet("{term}")]
        public async Task<IActionResult> GetNugetResults(string term) =>
            await _nugetService.TryGetNugetPackagesAsync(term)
                .ToActionResult();
    }
}
