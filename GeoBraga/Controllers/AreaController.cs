using GeoBraga.Models.Requests.AreaRequests;
using GeoBraga.Models.Responses;
using GeoBraga.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeoBraga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IAreaRepository _areaRepository;

        public AreaController(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAreasAsync(int? id)
        {
            return Ok(await _areaRepository.GetAreas(id));
        }
        
        [HttpPost]
        public async Task<IActionResult> SavePolygonAsync([FromBody] List<AddAreaRequest> requests)
        {
            try
            {
                return Ok(await _areaRepository.SavePolygon(requests));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePolygonAsync([FromBody] UpdateAreaRequest request)
        {
            try
            {
                if (request != null)
                {
                    return Ok(await _areaRepository.UpdatePolygon(request));
                }

                return BadRequest("Invalid input");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteNodeAsync([FromBody] DeleteAreaRequest request)
        {
            try
            {
                if (request.Id > 0)
                {
                    AreaResponse node = await _areaRepository.DeleteArea(request);
                    if (node == null)
                        return NotFound();

                    return Ok(await _areaRepository.DeleteArea(request));
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
