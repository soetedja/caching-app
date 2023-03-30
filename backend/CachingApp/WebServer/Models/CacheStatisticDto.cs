namespace Api.Models
{
    public class CacheStatisticDto
    {
        public int TotalHits { get; set; }
        public int TotalMisses { get; set; }
        public List<int>? MemoryLayout { get; set; }
        public Dictionary<int, int>? HitFrequency { get; set; }
        // public Dictionary<int, DateTime>? CacheAuditLogs { get; set; }
    }
}
