namespace ArcProxy.Core.Data.Entities
{
    public class GeoServiceEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Uri { get; set; }
        public GeoServiceRuleEntity? Rule { get; set; }
    }
}
