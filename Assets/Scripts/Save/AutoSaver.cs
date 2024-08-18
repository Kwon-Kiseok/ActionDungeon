using System;
using System.Collections.Generic;
using UnityEngine;

public class AutoSaver : MonoBehaviour
{
    private List<ISaveable> _saveables = new List<ISaveable>();
    private float _throttlingTimer;
    
    public void Register(ISaveable saveable)
    {
        _saveables.Add(saveable);
    }

    public void Remove(ISaveable saveable)
    {
        _saveables.Remove(saveable);
    }

    private void Update()
    {
        if (_throttlingTimer >= 0f)
        {
            _throttlingTimer -= Time.deltaTime;   
        }
    }

    protected virtual void OnApplicationQuit ()
    {
        SaveAll();
    }

    public void SaveAll()
    {
        if(_throttlingTimer > 0) return;

        _throttlingTimer = 5f;
        foreach (var saveable in _saveables)
        {
            saveable.Save();
        }

        PlayerPrefs.Save();
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if(!hasFocus) SaveAll();
    }

    private void OnApplicationPause(bool pause)
    {
        if(pause) SaveAll();
    }
}
