using GeoBraga.Models.Requests.RouteRequests;
using GeoBraga.Models.Responses;

namespace GeoBraga.Repositories
{
    public interface IRouteRepository
    {
        Task<RouteResponse> DeleteRoute(DeleteRouteRequest request);
        Task<List<RouteResponse>> GetRoutes(int? id);
        Task<List<RouteResponse>> SaveLineString(List<AddRouteRequest> requests);
        Task<List<RouteResponse>> UpdateLineString(UpdateRouteRequest request);
    }
}