using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    const float width = 30;
    const float height = 28;

    public float Width
    {
        get
        {
            return width;
        }
    }

    public float Height
    {
        get
        {
            return height;
        }
    }

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update()
    {
		if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }
	}

    void MoveDown()
    {
        float newY = transform.position.y - 0.45f;
        transform.position = new Vector3(transform.position.x, newY, 0);
    }
}
