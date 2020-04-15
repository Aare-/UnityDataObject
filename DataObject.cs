using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataObject<T> : MonoBehaviour where T : class {
    [SerializeField] 
    protected T _Data; 
    
    private T _InitialisedData;
    
    private List<Action<T, T>> _Subscribers = new List<Action<T, T>>();
    
    public T Data {
        get => _InitialisedData;
        set {
            Debug.Log("Initialise with new data!");
            
            var oldDAta = _InitialisedData;
            
            _InitialisedData = value;
            _Data = _InitialisedData;

            foreach (var sub in _Subscribers)
                sub(oldDAta, _InitialisedData);
        }
    }

    public void Subscribe(Action<T, T> onDataChanged) {
        if (onDataChanged == null) return;
        if (_Subscribers.Contains(onDataChanged)) return;

        _Subscribers.Add(onDataChanged);

        onDataChanged(Data, Data);
    }

    public void Unsubscribe(Action<T, T> onVariableChanged) {
        _Subscribers.Remove(onVariableChanged);
    }
    
    protected void Update() {
        if (_Data != _InitialisedData) {
            Data = _Data;
        }
    }
}
