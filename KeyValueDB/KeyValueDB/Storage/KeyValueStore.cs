using System.Collections.Concurrent;

namespace KeyValueDb.Storage;

public class KeyValueStore
{
    private readonly ConcurrentDictionary<string, byte[]> data = new();

    public void Put(string key, byte[] value)
    {
        data[key] = value;
    }

    public bool TryGet(string key, out byte[]? value)
    {
        return data.TryGetValue(key, out value);
    }

    public bool Delete(string key)
    {
        return data.TryRemove(key, out _);
    }
}