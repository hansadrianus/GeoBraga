namespace GeoBraga.Models.Requests.AreaRequests;

[Serializable]
public class AddAreaRequest
{
    public string Name { get; set; }
    public List<LongLatRequest> Nodes { get; set; }
}