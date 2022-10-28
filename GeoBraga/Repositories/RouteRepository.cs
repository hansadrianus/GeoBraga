using GeoBraga.Data;
using GeoBraga.Entities;
using GeoBraga.Factories;
using GeoBraga.Models.Requests;
using GeoBraga.Models.Requests.AreaRequests;
using GeoBraga.Models.Requests.RouteRequests;
using GeoBraga.Models.Responses;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Algorithm;
using NetTopologySuite.Geometries;

namespace GeoBraga.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IQueryable<MasterRoute> _route;

        public RouteRepository(ApplicationDbContext context)
        {
            _context = context;
            _route = _context.Route.AsNoTracking();
        }

        public async Task<RouteResponse> DeleteRoute(DeleteRouteRequest request)
        {
            MasterRoute route = await _context.Route.FirstOrDefaultAsync(p => p.Id == request.Id);
            if (route == null)
                return null;

            _context.Remove(route);
            await _context.SaveChangesAsync();

            return MapToRouteResponse(route);
        }

        public async Task<List<RouteResponse>> GetRoutes(int? id)
        {
            List<MasterRoute> routes = await _route.ToListAsync();
            if (id != null)
            {
                routes = routes.Where(p => p.Id == id).ToList();
            }

            List<RouteResponse> responses = MapToRouteResponse(routes);

            return responses;
        }

        public async Task<List<RouteResponse>> SaveLineString(List<AddRouteRequest> requests)
        {
            List<RouteResponse> responses = new List<RouteResponse>();
            foreach (AddRouteRequest request in requests)
            {
                MasterRoute route = new MasterRoute()
                {
                    Name = request.Name,
                    LineNodes = PopulateLineString(request.Nodes)
                };

                _context.Add(route);
                responses.Add(MapToRouteResponse(route));
            }
            await _context.SaveChangesAsync();

            return responses;
        }

        public async Task<List<RouteResponse>> UpdateLineString(UpdateRouteRequest request)
        {
            List<RouteResponse> responses = new List<RouteResponse>();
            MasterRoute route = new MasterRoute()
            {
                Name = request.Name,
                LineNodes = PopulateLineString(request.Nodes),
                ModifiedBy = "",
                ModifiedTime = DateTime.UtcNow
            };

            _context.Update(route);
            responses.Add(MapToRouteResponse(route));
            await _context.SaveChangesAsync();

            return responses;
        }

        #region Private methods
        private List<RouteResponse> MapToRouteResponse(List<MasterRoute> routes)
        {
            List<RouteResponse> response = new List<RouteResponse>();
            foreach (MasterRoute route in routes)
            {
                response.Add(new RouteResponse()
                {
                    Id = route.Id,
                    Name = route.Name,
                    Properties = route.LineNodes
                });
            }

            return response;
        }

        private RouteResponse MapToRouteResponse(MasterRoute route)
        {
            RouteResponse response = new RouteResponse()
            {
                Name = route.Name,
                Properties = route.LineNodes
            };

            return response;
        }

        private LineString PopulateLineString(List<LongLatRequest> requestNodes)
        {
            GeometryFactoryConfig factory = new GeometryFactoryConfig();
            List<Coordinate> coordinates = new List<Coordinate>();
            foreach (LongLatRequest node in requestNodes)
            {
                coordinates.Add(new Coordinate(node.Longitude, node.Latitude));
            }

            return factory.CreateLineString(coordinates.ToArray());
        }
        #endregion
    }
}
