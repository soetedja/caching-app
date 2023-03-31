using System.Collections.Concurrent;

namespace Common
{
    public class Cache
    {
        private static readonly ConcurrentDictionary<int, CacheObject> cache = new();

        private readonly ConcurrentDictionary<int, List<DateTime>> cacheHitsHistory = new();
        private const int MAX_SIZE = 100;
        private int hits;
        private int misses;

        public Cache()
        {
        }

        public int Hits => hits;

        public int Misses => misses;

        public static List<int> MemoryAddressLayout
        {
            get
            {
                var sortedKeys = cache
                    .OrderByDescending(o => o.Value.LastAccessed)
                    .Select(s => s.Key)
                    .ToList();
                return sortedKeys;
            }
        }

        public bool Clear()
        {
            cache.Clear();
            cacheHitsHistory.Clear();
            hits = 0;
            misses = 0;
            return true;
        }

        public void Add(int key, object value)
        {
            if (cache.Count >= MAX_SIZE)
            {
                // If cache is full, remove the least accessed  item
                int oldestKey = GetLeastAccessedCacheKey();
                cache.Remove(oldestKey, out _);
                cacheHitsHistory.Remove(oldestKey, out _);
            }

            if (cache.TryGetValue(key, out CacheObject? existingValue))
            {
                CacheObject newValue = existingValue;
                newValue.LastAccessed = DateTime.Now;
                cache.TryUpdate(key, newValue, existingValue);
            }
            else
            {
                CacheObject newValue = new(value, DateTime.Now);
                cache.TryAdd(key, newValue);
            }
        }

        private int GetLeastAccessedCacheKey()
        {
            int key = cache.OrderBy(s => s.Value.LastAccessed).FirstOrDefault().Key;
            return key;
        }

        public bool TryGetValue(int key, out CacheObject? value)
        {
            var isFound = cache.TryGetValue(key, out value);

            if (isFound)
            {
                hits++;
                //cacheHitsHistory[key].Add(DateTime.Now);
                //cacheHitsHistory.AddOrUpdate(key, DateTime.Now, (key, oldValue) => oldValue);
                if (cacheHitsHistory.TryGetValue(key, out var existingValue))
                {
                    var newValue = existingValue;
                    newValue.Add(DateTime.Now);
                    cacheHitsHistory.TryUpdate(key, newValue, existingValue);
                }
                else
                {
                    List<DateTime> newValue = new() { DateTime.Now };
                    cacheHitsHistory.TryAdd(key, newValue);
                }
            }
            else
            {
                misses++;
                cacheHitsHistory.TryAdd(key, new List<DateTime>());
            }

            return isFound;
        }

        public Dictionary<int, int> GetFrequency()
        {
            var result = new Dictionary<int, int>();

            foreach (var cacheHistory in cacheHitsHistory)
            {
                var history = cacheHistory.Value?.Where(s => (DateTime.Now - s) <= TimeSpan.FromSeconds(5));
                if(history != null)
                {
                    result[cacheHistory.Key] = history.Count();
                }
            }

            return result;
        }
    }

}