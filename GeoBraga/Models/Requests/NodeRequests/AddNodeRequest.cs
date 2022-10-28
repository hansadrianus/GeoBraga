namespace GeoBraga.Models.Requests.NodeRequests
{
    [Serializable]
    public class AddNodeRequest
    {
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
