namespace Common
{
    public class CacheObject
    {
        public object Value { get; }

        public DateTime LastAccessed { get; set; }

        public CacheObject(object value, DateTime lastAccessed)
        {
            Value = value;
            LastAccessed = lastAccessed;
        }
    }
}
