using GeoBraga.Models.Requests.RouteRequests;
using GeoBraga.Models.Responses;
using GeoBraga.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeoBraga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteRepository _routeRepository;

        public RouteController(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetroutesAsync(int? id)
        {
            return Ok(await _routeRepository.GetRoutes(id));
        }

        [HttpPost]
        public async Task<IActionResult> SavePolygonAsync([FromBody] List<AddRouteRequest> requests)
        {
            try
            {
                return Ok(await _routeRepository.SaveLineString(requests));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePolygonAsync([FromBody] UpdateRouteRequest request)
        {
            try
            {
                if (request != null)
                {
                    return Ok(await _routeRepository.UpdateLineString(request));
                }

                return BadRequest("Invalid input");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteNodeAsync([FromBody] DeleteRouteRequest request)
        {
            try
            {
                if (request.Id > 0)
                {
                    RouteResponse node = await _routeRepository.DeleteRoute(request);
                    if (node == null)
                        return NotFound();

                    return Ok(await _routeRepository.DeleteRoute(request));
                }

                return BadRequest("Invalid input");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
