// 2025/12/3 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int width = 10; // Maze width
    public int height = 10; // Maze height
    public GameObject wallPrefab; // Wall prefab to instantiate

    private int[,] maze; // Maze grid

    void Start()
    {
        GenerateMaze();
        RenderMaze();
    }

    void GenerateMaze()
    {
        maze = new int[width, height];

        // Initialize all cells as walls
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                maze[x, y] = 1;
            }
        }

        // Start recursive backtracking from a random point
        int startX = Random.Range(0, width);
        int startY = Random.Range(0, height);
        maze[startX, startY] = 0; // Make the starting point a path
        CarvePath(startX, startY);
    }

    void CarvePath(int x, int y)
    {
        // Directions: Up, Down, Left, Right
        int[][] directions = new int[][]
        {
            new int[] { 0, 1 },
            new int[] { 0, -1 },
            new int[] { 1, 0 },
            new int[] { -1, 0 }
        };

        // Shuffle directions
        for (int i = 0; i < directions.Length; i++)
        {
            int randomIndex = Random.Range(0, directions.Length);
            int[] temp = directions[i];
            directions[i] = directions[randomIndex];
            directions[randomIndex] = temp;
        }

        foreach (var dir in directions)
        {
            int newX = x + dir[0] * 2;
            int newY = y + dir[1] * 2;

            if (IsInBounds(newX, newY) && maze[newX, newY] == 1)
            {
                maze[newX - dir[0], newY - dir[1]] = 0; // Remove wall between cells
                maze[newX, newY] = 0; // Mark new cell as a path
                CarvePath(newX, newY);
            }
        }
    }

    bool IsInBounds(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    void RenderMaze()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (maze[x, y] == 1)
                {
                    Vector3 position = new Vector3(x, 0, y);
                    Instantiate(wallPrefab, position, Quaternion.identity);
                }
            }
        }
    }
}