using Microsoft.AspNetCore.Mvc;
using Swapfiets.BikeThefts.Models;
using Swapfiets.BikeThefts.Services;

namespace Swapfiets.BikeThefts.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BikeTheftsController : ControllerBase
    {
        readonly BikeTheftsService _bikeTheftsService;

        public BikeTheftsController(BikeTheftsService bikeTheftsService)
        {
            _bikeTheftsService = bikeTheftsService;
        }

        /// <summary>
        /// Retrieve the count of bike thefts in the provided city.
        /// </summary>
        /// <param name="parameters">
        /// - City: The city to find the bike theft count
        /// - CityRadiusInMiles: The radius distance in miles to cover the bike theft's count, starting on the city center. Default: 10 miles.
        /// </param>
        /// <returns>Bike theft's count</returns>
        [HttpGet("Count")]
        public async Task<IActionResult> GetBikeTheftsCount([FromQuery] SearchParameters parameters)
        {
            var requestIsValid = parameters.IsValid();
            if (requestIsValid.IsFailure)
            {
                return BadRequest(requestIsValid.Error);
            }

            var bikeTheftsResult = await _bikeTheftsService.GetBikeTheftsCount(parameters.City, parameters.CityRadiusInMiles);
            if (bikeTheftsResult.IsFailure)
            {
                return NotFound(bikeTheftsResult.Error);
            }

            return Ok(bikeTheftsResult.Value);
        }

        /// <summary>
        /// Retrieve the information for the current cities in operation and the candidate cities, with their bike theft's count.
        /// </summary>
        /// <returns>Cities in operation and candidate cities</returns>
        [HttpGet("CurrentAndCandidateCities")]
        public async Task<IActionResult> GetBikeTheftsCountForCities()
        {
            var bikeTheftsResult = await _bikeTheftsService.GetBikeTheftsCountForCities();

            return Ok(bikeTheftsResult);
        }
    }
}