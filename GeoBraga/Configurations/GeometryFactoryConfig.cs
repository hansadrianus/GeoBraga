using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace GeoBraga.Factories
{
    public class GeometryFactoryConfig
    {
        private readonly GeometryFactory _geometryFactory;

        public GeometryFactoryConfig()
        {
            _geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
        }

        public Point CreatePoint(double x, double y)
        {
            return _geometryFactory.CreatePoint(new Coordinate(x, y));
        }

        public Polygon CreatePolygon(Coordinate[] coordinates)
        {
            return _geometryFactory.CreatePolygon(coordinates);
        }

        public LineString CreateLineString(Coordinate[] coordinates)
        {
            return _geometryFactory.CreateLineString(coordinates);
        }
    }
}
