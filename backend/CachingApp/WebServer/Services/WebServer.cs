using Common;

namespace Api.Services
{
    public class WebServer: IWebServer
    {
        private readonly Cache _cache;

        public WebServer(Cache cache)
        {
            _cache = cache;
        }

        public int GetModNumber(int num)
        {
            if(!_cache.TryGetValue(num, out var mod))
            {
                mod = num % 1234;
                _cache.Add(num, mod);
            }

            return mod;
        }

        public Cache GetCache()
        {
            return _cache;
        }
    }
}
