using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]
public class HPBarSlider : MonoBehaviour
{
    /// <summary>
    /// Adjusts the Fill Value of an image
    /// -Up to you to design the image or slider. It only adjust the fill
    /// </summary>

    [Range(0, 1)]
    public float currentValue;
    public Image Fill;

    private float maxValue = 1;
    private void Awake()
    {
        currentValue = 1;
    }
    private void Update()
    {
        
        Fill.fillAmount = currentValue / maxValue;

    }

    public void UpdateValue(float currentValue, float maxValue)
    {
        this.currentValue = currentValue;
        this.maxValue = maxValue;
    }
}
