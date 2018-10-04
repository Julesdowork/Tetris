using System.Collections;
using UnityEngine;

public class Group : MonoBehaviour
{
    float lastFall = 0;     // time since last gravity tick
    Spawner spawner;

    void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
    }

    void Start()
	{
		// Default position not valid? Then it's game over
        if (!IsValidGridPosition())
        {
            Debug.Log("GAME OVER!!!");
            Destroy(gameObject);
        }
	}
	
	void Update()
	{
		// Move left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Modify position
            transform.position += new Vector3(-1, 0, 0);

            // See if valid
            if (IsValidGridPosition())
            {
                // It's valid. Update grid.
                UpdateGrid();
            }
            else
            {
                // Its not valid. Revert.
                transform.position += new Vector3(1, 0, 0);
            }
        }
        // Move right
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            
            if (IsValidGridPosition())
            {
                UpdateGrid();
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        // Rotate Left
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, -90);
            
            if (IsValidGridPosition())
            {
                UpdateGrid();
            }
            else
            {
                if (transform.position.x > 5f)
                    transform.position += new Vector3(-1f, 0, 0);
                else
                    transform.position += new Vector3(1f, 0, 0);

                UpdateGrid();
            }
        }
        // Rotate Right
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            transform.Rotate(0, 0, 90);

            if (IsValidGridPosition())
            {
                UpdateGrid();
            }
            else
            {
                if (transform.position.x > 5f)
                    transform.position += new Vector3(-1f, 0, 0);
                else
                    transform.position += new Vector3(1f, 0, 0);

                UpdateGrid();
            }
        }
        // Fall
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - lastFall >= 1)
        {
            transform.position += new Vector3(0, -1, 0);

            if (IsValidGridPosition())
            {
                UpdateGrid();
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);

                // Clear filled rows
                GameGrid.DeleteFullRows();

                // Spawn next group
                spawner.SpawnNext();

                // Disable script
                enabled = false;
            }

            lastFall = Time.time;
        }
        // Hard Drop
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            HardFall();
        }
    }

    bool IsValidGridPosition()
    {
        foreach (Transform child in transform)
        {
            Vector2 roundedVector = GameGrid.RoundVector2(child.position);

            // Not inside border?
            if (!GameGrid.InsideBorders(roundedVector))
                return false;

            // Block in grid cell (and not part of same group)?
            if (GameGrid.grid[(int)roundedVector.x, (int)roundedVector.y] != null &&
                GameGrid.grid[(int)roundedVector.x, (int)roundedVector.y].parent != transform)
                return false;
        }

        return true;
    }

    void UpdateGrid()
    {
        // Remove old children from grid
        for (int y = 0; y < GameGrid.height; y++)
        {
            for (int x = 0; x < GameGrid.width; x++)
            {
                if (GameGrid.grid[x, y] != null)
                {
                    if (GameGrid.grid[x, y].parent == transform)
                        GameGrid.grid[x, y] = null;
                }
            }
        }

        // Add new children to grid
        foreach (Transform child in transform)
        {
            Vector2 roundedVector = GameGrid.RoundVector2(child.position);
            GameGrid.grid[(int)roundedVector.x, (int)roundedVector.y] = child;
        }
    }

    void HardFall()
    {
        while (IsValidGridPosition())
        {
            UpdateGrid();
            transform.position += new Vector3(0, -1f, 0);
        }

        transform.position += new Vector3(0, 1f, 0);
        GameGrid.DeleteFullRows();
        spawner.SpawnNext();
        enabled = false;
        lastFall = Time.time;
    }
}