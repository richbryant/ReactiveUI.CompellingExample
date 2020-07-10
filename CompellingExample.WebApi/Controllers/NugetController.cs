using System;
using System.Threading.Tasks;
using CompellingExample.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompellingExample.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NugetController : ControllerBase
    {
        private readonly INugetService _nugetService;

        public NugetController(INugetService nugetService)
        {
            _nugetService = nugetService;
        }

        [HttpGet("{term}")]
        public async Task<IActionResult> GetNugetResults(string term)
        {
            try
            {
                var result = await _nugetService.GetNugetPackages(term);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
