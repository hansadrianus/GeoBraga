using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;

namespace GeoBraga.Entities
{
    [Table("Route")]
    public class MasterRoute : AuditableEntity
    {
        public string Name { get; set; }
        public LineString LineNodes { get; set; }
    }
}
