namespace ArcProxy.Core.Models.DTOs
{
    public class GeoServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ServiceUri { get; set; }
        public int RequestLimit { get; set; }
        public int RequestCount { get; set; }
    }
}
