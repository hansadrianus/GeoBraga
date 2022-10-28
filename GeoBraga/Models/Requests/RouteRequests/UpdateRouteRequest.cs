namespace GeoBraga.Models.Requests.RouteRequests
{
    [Serializable]
    public class UpdateRouteRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<LongLatRequest> Nodes { get; set; }
    }
}
