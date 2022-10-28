using GeoBraga.Models.Requests.NodeRequests;
using GeoBraga.Models.Responses;

namespace GeoBraga.Repositories
{
    public interface INodeRepository
    {
        Task<NodeResponse> DeleteNode(DeleteNodeRequest request);
        Task<List<NodeResponse>> GetNodes(int? id);
        Task<List<NodeResponse>> SaveNode(List<AddNodeRequest> requests);
    }
}