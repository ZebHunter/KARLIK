using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EventBus
{
    public static EventBus Instance { get; private set; }

    private Dictionary<string, List<(Delegate callback, Delegate filter)>> _signalCallbacks;

    public EventBus()
    {
        Instance = this;
        _signalCallbacks = new Dictionary<string, List<(Delegate, Delegate)>>();
    }

    public void Subscribe<T>(Action<T> callback, Func<T, bool> filter = null)
    {
        string key = typeof(T).Name;

        if (!_signalCallbacks.ContainsKey(key))
            _signalCallbacks[key] = new List<(Delegate, Delegate)>();

        _signalCallbacks[key].Add((callback, filter));
    }

    public void Unsubscribe<T>(Action<T> callback)
    {
        string key = typeof(T).Name;
        if (_signalCallbacks.TryGetValue(key, out var list))
        {
            list.RemoveAll(entry => entry.callback.Equals(callback));
            if (list.Count == 0)
                _signalCallbacks.Remove(key);
        } else
        {
        }
    }
    
    public void Invoke<T>(T signal)
    {
        string key = typeof(T).Name;
        if (_signalCallbacks.TryGetValue(key, out var list))
        {
            var listCopy = new List<(Delegate callback, Delegate filter)>(list);
            foreach (var (callback, filter) in listCopy)
            {
                var typedCallback = callback as Action<T>;
                var typedFilter = filter as Func<T, bool>;

                if (typedFilter == null || typedFilter(signal))
                    typedCallback?.Invoke(signal);
            }
        }
    }
}
