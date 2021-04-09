using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Requires button to be in gameobject
public class ButtonPressed : MonoBehaviour
{
    private Image buttonImage;
    private Color initialColor;
    private bool isPressed;
    public Color PressedColor;
   
    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        initialColor = buttonImage.color;
    }

    public void OnButtonPressed()
    {
       if(isPressed) 
        {
            isPressed = false;
            buttonImage.color = initialColor;
        }
       else
        {
            isPressed = true;
            buttonImage.color = PressedColor;
        }
    }
}
