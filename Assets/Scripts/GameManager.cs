using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // The Grid itself
    public static int WIDTH = 10;
    public static int HEIGHT = 20;

    public Transform[,] grid = new Transform[WIDTH, HEIGHT];
    
    Score score;

    void Awake()
    {
        score = FindObjectOfType<Score>();
    }

    public Vector2 RoundVector2(Vector2 vector)
    {
        return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
    }

    public bool InsideBorders(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < WIDTH && (int)pos.y >= 0);
    }

    public void DeleteRow(int y)
    {
        for (int x = 0; x < WIDTH; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public void DecreaseRow(int y)
    {
        for (int x = 0; x < WIDTH; x++)
        {
            if (grid[x, y] != null)
            {
                // Move one towards bottom
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                // Update Block position
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public void DecreaseRowsAbove(int y)
    {
        // Decrease rows above the specified row y
        for (int i = y + 1; i < HEIGHT; i++)
        {
            DecreaseRow(i);
        }
    }

    public bool IsRowFull(int y)
    {
        for (int x = 0; x < WIDTH; x++)
        {
            if (grid[x, y] == null)
                return false;
        }
        return true;
    }

    public void DeleteFullRows()
    {
        int fullRows = 0;
        for (int y = 0; y < HEIGHT; y++)
        {
            if (IsRowFull(y))
            {
                fullRows++;
                DeleteRow(y);
                DecreaseRowsAbove(y);
                y--;
            }
        }

        if (fullRows == 1) { score.AddScore(100); }
        else if (fullRows == 2) { score.AddScore(300); }
        else if (fullRows >= 3) { score.AddScore(500); }
    }
}