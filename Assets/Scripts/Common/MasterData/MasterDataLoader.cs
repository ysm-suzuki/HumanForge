using System.Collections.Generic;

public class MasterDataLoader<T>
{
    protected static Dictionary<int, T> _data = null;

    virtual public Dictionary<int, T> GetAllData()
    {
        return _data;
    }

    virtual public IEnumerable<T> GetAll()
    {
        return GetAllData().Values;
    }

    virtual public T Get(int id)
    {
        var data = GetAllData();

        UnityEngine.Debug.Assert(data.ContainsKey(id), "Invalid id.");

        return data[id];
    }

    virtual public void PreLoad()
    {
        GetAllData();
    }
}
