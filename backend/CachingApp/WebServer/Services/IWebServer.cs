using Common;

namespace Api.Services
{
    public interface IWebServer
    {
        int GetModNumber(int num);

        Cache GetCache();

        bool ClearCache();
    }
}
