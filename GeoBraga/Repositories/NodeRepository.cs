using GeoBraga.Data;
using GeoBraga.Entities;
using GeoBraga.Factories;
using GeoBraga.Models.Requests.NodeRequests;
using GeoBraga.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace GeoBraga.Repositories
{
    public class NodeRepository : INodeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IQueryable<MasterNode> _node;

        public NodeRepository(ApplicationDbContext context)
        {
            _context = context;
            _node = _context.Node.AsNoTracking();
        }

        public async Task<NodeResponse> DeleteNode(DeleteNodeRequest request)
        {
            MasterNode node = await _context.Node.FirstOrDefaultAsync(p => p.Id == request.Id);
            if (node == null)
                return null;

            _context.Remove(node);
            await _context.SaveChangesAsync();

            return MapToNodeResponse(node);
        }

        public async Task<List<NodeResponse>> GetNodes(int? id)
        {
            var nodes = await _node.ToListAsync();
            if (id != null)
            {
                nodes = nodes.Where(p => p.Id == id).ToList();
            }
            List<NodeResponse> response = MapToNodeResponse(nodes);

            return response;
        }

        public async Task<List<NodeResponse>> SaveNode(List<AddNodeRequest> requests)
        {
            List<NodeResponse> responses = new List<NodeResponse>();
            GeometryFactoryConfig factory = new GeometryFactoryConfig();
            foreach (AddNodeRequest request in requests)
            {
                MasterNode node = new MasterNode()
                {
                    Name = request.Name,
                    Node = factory.CreatePoint(request.Longitude, request.Latitude)
                };

                _context.Add(node);
                responses.Add(MapToNodeResponse(node));
            }
            await _context.SaveChangesAsync();

            return responses;
        }

        #region Private methods
        private List<NodeResponse> MapToNodeResponse(List<MasterNode> nodes)
        {
            List<NodeResponse> response = new List<NodeResponse>();
            foreach (MasterNode node in nodes)
            {
                response.Add(new NodeResponse()
                {
                    Id = node.Id,
                    Name = node.Name,
                    Properties = node.Node
                });
            }

            return response;
        }

        private NodeResponse MapToNodeResponse(MasterNode node)
        {
            NodeResponse response = new NodeResponse()
            {
                Name = node.Name,
                Properties = node.Node
            };

            return response;
        }
        #endregion
    }
}
