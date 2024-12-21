namespace ArcProxy.Core.Models
{
    public class CachedGeoServiceStat
    {
        public string ServiceUri { get; set; }
        public int RequestLimit { get; set; }
        public int RequestCount { get; set; }
        public bool IsServiceFree { get; set; }
    }
}
