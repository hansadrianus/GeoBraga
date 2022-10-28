using GeoBraga.Data;
using GeoBraga.Entities;
using GeoBraga.Factories;
using GeoBraga.Models.Requests;
using GeoBraga.Models.Requests.AreaRequests;
using GeoBraga.Models.Responses;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace GeoBraga.Repositories;

public class AreaRepository : IAreaRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IQueryable<MasterArea> _area;

    public AreaRepository(ApplicationDbContext context)
    {
        _context = context;
        _area = _context.Area.AsNoTracking();
    }
    
    public async Task<AreaResponse> DeleteArea(DeleteAreaRequest request)
    {
        MasterArea area = await _context.Area.FirstOrDefaultAsync(p => p.Id == request.Id);
        if (area == null)
            return null;

        _context.Remove(area);
        await _context.SaveChangesAsync();

        return MapToAreaResponse(area);
    }

    public async Task<List<AreaResponse>> GetAreas(int? id)
    {
        List<MasterArea> areas = await _area.ToListAsync();
        if (id != null)
        {
            areas = areas.Where(p => p.Id == id).ToList();
        }

        List<AreaResponse> responses = MapToAreaResponse(areas);

        return responses;
    }

    public async Task<List<AreaResponse>> SavePolygon(List<AddAreaRequest> requests)
    {
        List<AreaResponse> responses = new List<AreaResponse>();
        foreach (AddAreaRequest request in requests)
        {
            MasterArea area = new MasterArea()
            {
                Name = request.Name,
                PolygonNodes = PopulatePolygon(request.Nodes)
            };
            
            _context.Add(area);
            responses.Add(MapToAreaResponse(area));
        }
        await _context.SaveChangesAsync();
        
        return responses;
    }
    
    public async Task<List<AreaResponse>> UpdatePolygon(UpdateAreaRequest request)
    {
        List<AreaResponse> responses = new List<AreaResponse>();
        MasterArea area = new MasterArea()
        {
            Name = request.Name,
            PolygonNodes = PopulatePolygon(request.Nodes),
            ModifiedBy = "",
            ModifiedTime = DateTime.UtcNow
        };
        
        _context.Update(area);
        responses.Add(MapToAreaResponse(area));
        await _context.SaveChangesAsync();
        
        return responses;
    }

    #region Private methods
    private List<AreaResponse> MapToAreaResponse(List<MasterArea> areas)
    {
        List<AreaResponse> response = new List<AreaResponse>();
        foreach (MasterArea area in areas)
        {
            response.Add(new AreaResponse()
            {
                Id = area.Id,
                Name = area.Name,
                Properties = area.PolygonNodes
            });
        }

        return response;
    }

    private AreaResponse MapToAreaResponse(MasterArea area)
    {
        AreaResponse response = new AreaResponse()
        {
            Name = area.Name,
            Properties = area.PolygonNodes
        };

        return response;
    }
    
    private Polygon PopulatePolygon(List<LongLatRequest> requestNodes)
    {
        GeometryFactoryConfig factory = new GeometryFactoryConfig();
        List<Coordinate> coordinates = new List<Coordinate>();
        foreach (LongLatRequest node in requestNodes)
        {
            coordinates.Add(new Coordinate(node.Longitude, node.Latitude));
        }

        return factory.CreatePolygon(coordinates.ToArray());
    }
    #endregion
}