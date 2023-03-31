using Common;

namespace Api.Services
{
    public class WebServer : IWebServer
    {
        private readonly Cache _cache;

        public WebServer(Cache cache)
        {
            _cache = cache;
        }

        public int GetModNumber(int num)
        {
            int modNumber;

            if (!_cache.TryGetValue(num, out var mod))
            {
                modNumber = num % 1234;
                _cache.Add(num, modNumber);
            }
            else
            {
                modNumber = (int)mod.Value;
            }

            return modNumber;
        }

        public Cache GetCache()
        {
            return _cache;
        }

        public bool ClearCache()
        {
            return _cache.Clear();
        }
    }
}
