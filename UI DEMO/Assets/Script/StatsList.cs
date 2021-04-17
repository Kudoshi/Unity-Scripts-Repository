using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Handle creation of stat holder prefabs
/// </summary>
public class StatsList : MonoBehaviour
{
    [SerializeField] private StatHolder _statHolderPrefab;
    [SerializeField] private Transform _statHolderRoot;

    private Dictionary<StatType, StatHolder> _holders = new Dictionary<StatType, StatHolder>();
    private Stats _boundStats;

    private void Awake()
    {
        _statHolderPrefab.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (_boundStats != null)
            _boundStats.OnStatChanged -= HandleStatChanged;
    }

    public void Bind(Stats stats)
    {
        //Check if current stat is same as previous
        if (_boundStats == stats)
        {
            if (_boundStats == null)
                _statHolderRoot.gameObject.SetActive(false);
            
            return;
        }
        //All these execute if new entity

        //Unsubs from old stats
        if (_boundStats != null)
            _boundStats.OnStatChanged -= HandleStatChanged;
        
        _boundStats = stats;
        
        //Clear old stat holders
        foreach(var holder in _holders.Values)
            Destroy(holder.gameObject);
        
        _holders.Clear();
        
        //Initialize new stat holders
        if (_boundStats != null)
        {
            _boundStats.OnStatChanged += HandleStatChanged;
            _statHolderRoot.gameObject.SetActive(true);
            foreach (var stat in _boundStats.RuntimeStatValues)
            {
                CreateStatHolder(stat);
            }
        }
        else
        {
            _statHolderRoot.gameObject.SetActive(false);
        }
    }

    private void HandleStatChanged(StatData statData)
    {
        if (_holders.TryGetValue(statData.StatType, out var existingStatData))
            existingStatData.SetData(_boundStats, statData);
        else
            CreateStatHolder(statData);
    }

    private void CreateStatHolder(StatData statData)
    {
        var statHolder = Instantiate(_statHolderPrefab, _statHolderRoot);
        statHolder.SetData(_boundStats, statData);
        statHolder.gameObject.SetActive(true);
        _holders.Add(statData.StatType, statHolder);
    }
}

[Serializable]
public class StatData
{
    public StatType StatType;
    public int Value;
}

public enum StatType
{
    Strength,
    Speed,
    Damage,
    MaxHealth,
}

//public static class UIController
//{
//    private static Stats _boundStats;
//    private static StatsList _statsList;
//
//    public static void Bind(Stats stats)
//    {
//        _boundStats = stats;
//        if (_statsList != null)
//            _statsList.Bind(_boundStats);
//    }
//
//    public static void Bind(StatsList statsList)
//    {
//        _statsList = statsList;
//        if (_statsList != null) 
//            _statsList.Bind(_boundStats);
//    }
//}