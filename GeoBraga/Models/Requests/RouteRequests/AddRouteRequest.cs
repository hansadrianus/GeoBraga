namespace GeoBraga.Models.Requests.RouteRequests
{
    [Serializable]
    public class AddRouteRequest
    {
        public string Name { get; set; }
        public List<LongLatRequest> Nodes { get; set; }
    }
}
