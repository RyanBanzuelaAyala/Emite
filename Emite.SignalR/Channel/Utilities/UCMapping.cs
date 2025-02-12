namespace Emite.SignalR.Channel.Utilities;
public class UCMapping<T>
{
    private readonly Dictionary<T, HashSet<string>> _connections = new Dictionary<T, HashSet<string>>();

    public int Count
    {
        get
        {
            return _connections.Count;
        }
    }

    public bool IsExist(string connectionId)
    {
        var hasData = _connections.Where(s => s.Equals(connectionId)).FirstOrDefault();
        return hasData.Value == null ? false : true;

    }

    public List<string> GetAllConnected
    {
        get
        {
            var newList = new List<string>();

            foreach (var item in _connections)
            {
                newList.Add(item.Key.ToString());
            }

            return newList;
        }
    }

    public void Add(T key, string connectionId)
    {
        lock (_connections)
        {
            HashSet<string> connections;

            if (!_connections.TryGetValue(key, out connections))
            {
                connections = new HashSet<string>();
                _connections.Add(key, connections);
            }

            lock (connections)
            {
                connections.Add(connectionId);
            }
        }
    }

    public void Remove(T key, string connectionId)
    {
        lock (_connections)
        {
            HashSet<string> connections;

            if (!_connections.TryGetValue(key, out connections))
            {
                return;
            }

            lock (connections)
            {
                connections.Remove(connectionId);

                if (connections.Count == 0)
                {
                    _connections.Remove(key);
                }
            }
        }
    }

    public IEnumerable<string> GetConnections(T key)
    {
        HashSet<string> connections;

        if (_connections.TryGetValue(key, out connections))
        {
            return connections;
        }

        return Enumerable.Empty<string>();
    }

}
