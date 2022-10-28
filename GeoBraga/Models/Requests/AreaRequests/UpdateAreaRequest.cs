using NetTopologySuite.Geometries;

namespace GeoBraga.Models.Requests.AreaRequests;

[Serializable]
public class UpdateAreaRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<LongLatRequest> Nodes { get; set; }
}