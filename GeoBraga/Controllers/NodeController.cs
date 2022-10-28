using GeoBraga.Models.Requests.NodeRequests;
using GeoBraga.Models.Responses;
using GeoBraga.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GeoBraga.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NodeController : ControllerBase
    {
        private readonly INodeRepository _nodeRepository;

        public NodeController(INodeRepository nodeRepository)
        {
            _nodeRepository = nodeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetNodesAsync(int? id)
        {
            return Ok(await _nodeRepository.GetNodes(id));
        }

        [HttpPost]
        public async Task<IActionResult> SaveNodeAsync([FromBody] List<AddNodeRequest> requests)
        {
            try
            {
                return Ok(await _nodeRepository.SaveNode(requests));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteNodeAsync([FromBody] DeleteNodeRequest request)
        {
            try
            {
                if (request.Id > 0)
                {
                    NodeResponse node = await _nodeRepository.DeleteNode(request);
                    if (node == null)
                        return NotFound();

                    return Ok(await _nodeRepository.DeleteNode(request));
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
