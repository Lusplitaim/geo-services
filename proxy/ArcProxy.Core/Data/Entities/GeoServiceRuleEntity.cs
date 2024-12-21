namespace ArcProxy.Core.Data.Entities
{
    public class GeoServiceRuleEntity
    {
        public int Id { get; set; }
        public int RequestLimit { get; set; }
        public int ServiceId { get; set; }
        public GeoServiceEntity Service { get; set; }
    }
}
