using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SerializationDictionary<TKey, TValue> where TKey : notnull
{
    [SerializeField] private List<TKey> keys;
    [SerializeField] private List<TValue> values;

    public Dictionary<TKey, TValue> ToDictionary()
    {
        var dictionary = new Dictionary<TKey, TValue>();
        for (int i = 0; i < keys.Count; i++)
        {
            dictionary[keys[i]] = values[i];
        }
        return dictionary;
    }

    public SerializationDictionary(Dictionary<TKey, TValue> dictionary)
    {
        keys = new List<TKey>();
        values = new List<TValue>();
        foreach (var kvp in dictionary)
        {
            keys.Add(kvp.Key);
            values.Add(kvp.Value);
        }
    }
}
