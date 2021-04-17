using TMPro;
using UnityEngine;
/// <summary>
/// Handles display of stats 
/// Handle input of stat
/// </summary>
public class StatHolder : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _value;

    private StatData _statData;
    private Stats _stats;

    public void SetData(Stats stats, StatData statData)
    {
        //Copy reference of stat class and statdata
        _stats = stats;
        _statData = statData;

        //Update UI
        _label.SetText(statData.StatType.ToString());
        _value.SetText(statData.Value.ToString());
    }

    public void AddStat()
    {
        _stats.Modify(_statData.StatType, 1);
    }
}