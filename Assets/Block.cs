using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    const float width = 30;
    const float height = 28;

    public bool atRest = false;

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
        StartCoroutine(Fall());
	}
	
	// Update is called once per frame
	void Update()
    {
		if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }

        MoveHorizontal();
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with " + collision.collider.name);
        if (collision.collider.CompareTag("Boundary"))
            atRest = true;
    }

    void MoveDown()
    {
        float newY = transform.position.y - 1f;
        transform.position = new Vector3(transform.position.x, newY, 0);
    }

    void MoveHorizontal()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            float direction = Input.GetAxisRaw("Horizontal");
            float newX = transform.position.x + direction;
            transform.position = new Vector3(newX, transform.position.y, 0);
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(1f);
        while (!atRest)
        {
            MoveDown();
            yield return new WaitForSeconds(1f);
        }
    }
}
