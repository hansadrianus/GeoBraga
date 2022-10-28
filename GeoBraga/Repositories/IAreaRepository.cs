using GeoBraga.Models.Requests.AreaRequests;
using GeoBraga.Models.Responses;

namespace GeoBraga.Repositories;

public interface IAreaRepository
{
    Task<AreaResponse> DeleteArea(DeleteAreaRequest request);
    Task<List<AreaResponse>> GetAreas(int? id);
    Task<List<AreaResponse>> SavePolygon(List<AddAreaRequest> requests);
    Task<List<AreaResponse>> UpdatePolygon(UpdateAreaRequest request);
}