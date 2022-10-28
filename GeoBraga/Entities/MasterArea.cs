using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;

namespace GeoBraga.Entities
{
    [Table("Area")]
    public class MasterArea : AuditableEntity
    {
        public string Name { get; set; }
        public Polygon PolygonNodes { get; set; }
    }
}
