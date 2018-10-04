using System.Collections;
using UnityEngine;

public class Group : MonoBehaviour
{
    float lastFall = 0;     // time since last gravity tick
    Spawner spawner;
    GameManager gameManager;
    Score score;

    void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
        gameManager = FindObjectOfType<GameManager>();
        score = FindObjectOfType<Score>();
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
        // Soft Drop
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0, -1, 0);

            if (IsValidGridPosition())
            {
                UpdateGrid();
                score.AddScore(1);
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);

                // Clear filled rows
                gameManager.DeleteFullRows();

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
                gameManager.DeleteFullRows();

                // Spawn next group
                spawner.SpawnNext();

                // Disable script
                enabled = false;
            }

            lastFall = Time.time;
        }
    }

    bool IsValidGridPosition()
    {
        foreach (Transform child in transform)
        {
            Vector2 roundedVector = gameManager.RoundVector2(child.position);

            // Not inside border?
            if (!gameManager.InsideBorders(roundedVector))
                return false;

            // Block in grid cell (and not part of same group)?
            if (gameManager.grid[(int)roundedVector.x, (int)roundedVector.y] != null &&
                gameManager.grid[(int)roundedVector.x, (int)roundedVector.y].parent != transform)
                return false;
        }

        return true;
    }

    void UpdateGrid()
    {
        // Remove old children from grid
        for (int y = 0; y < GameManager.HEIGHT; y++)
        {
            for (int x = 0; x < GameManager.WIDTH; x++)
            {
                if (gameManager.grid[x, y] != null)
                {
                    if (gameManager.grid[x, y].parent == transform)
                        gameManager.grid[x, y] = null;
                }
            }
        }

        // Add new children to grid
        foreach (Transform child in transform)
        {
            Vector2 roundedVector = gameManager.RoundVector2(child.position);
            gameManager.grid[(int)roundedVector.x, (int)roundedVector.y] = child;
        }
    }

    void HardFall()
    {
        int distance = 0;
        while (IsValidGridPosition())
        {
            UpdateGrid();
            transform.position += new Vector3(0, -1f, 0);
            distance++;
        }

        transform.position += new Vector3(0, 1f, 0);
        distance--;
        score.AddScore(2 * distance);

        gameManager.DeleteFullRows();
        spawner.SpawnNext();
        enabled = false;
        lastFall = Time.time;
    }
}