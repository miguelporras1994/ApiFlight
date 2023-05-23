using flights.Domain.DTO;
using flights.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Flights.Api.Controllers
{
    public class FlightsController : Controller
    {
        private readonly IFlightsDomService _flightsIDVDomService;

        public FlightsController(IFlightsDomService flightsIDVDomService)
        {
            _flightsIDVDomService = flightsIDVDomService;
        }

        [Route("GetFlights")]
        [HttpPost]

        public async Task<ActionResult> GetFlights([BindRequired, FromBody] SearchFlightsDTO searchFlights)
        {




            var result = await _flightsIDVDomService.GetFlights(searchFlights);


            return Ok(result);
        }
    }
}
