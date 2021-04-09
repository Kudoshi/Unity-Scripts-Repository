using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid2D
{
    //Array size
    private int arrayWidth; //width 
    private int arrayHeight; //height
    private int[,] gridArray;

    private float cellSize;

    public Grid2D(int arrayWidth, int arrayHeight, float cellSize)
    {
        this.arrayWidth = arrayWidth;
        this.arrayHeight = arrayHeight;
        this.cellSize = cellSize;

        gridArray = new int[this.arrayWidth, this.arrayHeight];

        for (int width = 0; width < gridArray.GetLength(0); width++)
        {
            for (int height = 0; height < gridArray.GetLength(1); height++)
            {
                //Create a text in the array Point
                CreateWorldText(gridArray[width, height].ToString(), null, GetWorldPosition(width, height), 20, Color.white, TextAnchor.MiddleCenter, TextAlignment.Center);
                //Draw Height
                Debug.DrawLine(GetWorldPosition(width, height), GetWorldPosition(width, height + 1), Color.white, 100f);
                //Draw width
                Debug.DrawLine(GetWorldPosition(width, height), GetWorldPosition(width + 1, height), Color.white, 100f);

            }
        }

    }
    private TextMesh CreateWorldText(string text, Transform parent, Vector3 localPosition, int fontSize, Color colour, TextAnchor textAnchor, TextAlignment textAlignment)
    {
        //Creates gameobjects and define the attributes

        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        
        //Define transform attribute
        Transform transform = gameObject.transform;

        transform.SetParent(parent, false);
        transform.localPosition = localPosition;

        //Define textmesh attribute
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = colour;
        
        //Could not Find the component for it 
        //textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }
    private Vector3 GetWorldPosition(int arrWidth, int arrHeight)
    {
        return new Vector3(arrWidth, arrHeight) * cellSize;
    }
}
