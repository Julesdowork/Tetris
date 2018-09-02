using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    const int width = 30;
    const int height = 28;
    RectTransform rectTransform;

    public int Width
    {
        get
        {
            return width;
        }
    }

    public int Height
    {
        get
        {
            return height;
        }
    }

    // Use this for initialization
    void Start ()
    {
        rectTransform = GetComponent<RectTransform>();
        Debug.Log("Transform Position: " + rectTransform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void MoveDown()
    {

    }
}
