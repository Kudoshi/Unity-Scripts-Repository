using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Test2 : MonoBehaviour
{
    public Test SO;
    public TextMeshProUGUI tmp;
    public int value;

    private int currentDisplayed;
    // Start is called before the first frame update
    private void Awake()
    {
        value = SO.hello;
        currentDisplayed = -1;
    }
    void Start()
    {
        Invoke("ChangeValue", 2.5f);
    }

    public void ChangeValue()
    {
        SO.hello += 1;
        value = SO.hello;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDisplayed != value)
        {
            tmp.text = value.ToString();
        }
        
    }
}
