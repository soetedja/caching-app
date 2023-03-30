using System.Linq;

namespace Common
{
    public class Cache
    {
        private readonly Dictionary<int, int> cache;
        private readonly Dictionary<int, DateTime> cacheAuditLog;
        private readonly Dictionary<int, List<DateTime>> cacheHitsHistory;
        private readonly int maxSize;
        private int hits;
        private int misses;

        public Cache()
        {
            cache = new Dictionary<int, int>();
            cacheAuditLog = new Dictionary<int, DateTime>();
            cacheHitsHistory = new Dictionary<int, List<DateTime>>();
            maxSize = 100;
        }

        public int Hits => hits;

        public int Misses => misses;

        //public List<int> MemoryAddressLayout => cache.Select(s => s.Key).ToList();

        public List<int> MemoryAddressLayout
        {
            get
            {
                var sortedKeys = cacheAuditLog
                    .Where(c => cache.Select(s => s.Key).Contains(c.Key))
                    .OrderByDescending(pair => pair.Value)
                    .Select(pair => pair.Key)
                    .ToList();
                return sortedKeys;
            }
        }

        //public Dictionary<int, DateTime> CacheAuditLogs => cacheAuditLog;

        public void Add(int key, int value)
        {
            if (cache.Count >= maxSize)
            {
                // If cache is full, remove the least accessed  item
                int oldestKey = GetLeastAccessedCacheKey();
                cache.Remove(oldestKey);
                cacheHitsHistory.Remove(oldestKey);
                cacheAuditLog.Remove(oldestKey);
            }

            cache.Add(key, value);
            cacheAuditLog[key] = DateTime.Now;
        }

        private int GetLeastAccessedCacheKey()
        {
            int key = cacheAuditLog.OrderBy(s => s.Value).FirstOrDefault().Key;
            return key;
        }

        public bool TryGetValue(int key, out int value)
        {
            var isFound = cache.TryGetValue(key, out value);

            if (isFound)
            {
                hits++;
                cacheHitsHistory[key].Add(DateTime.Now);
                cacheAuditLog[key] = DateTime.Now;
            } else
            {
                cacheHitsHistory.Add(key, new List<DateTime>());
                misses++;
            }

            return isFound;
        }

        public Dictionary<int, int> GetFrequency()
        {
            var result = new Dictionary<int, int>();

            foreach (var cacheHistory in cacheHitsHistory)
            {
                var his = cacheHistory.Value.Where(s => (DateTime.Now - s) <= TimeSpan.FromHours(5));
                result[cacheHistory.Key] = his.Count();
            }

            return result;
        }
    }

}