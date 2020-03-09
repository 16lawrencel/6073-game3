using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinGen : MonoBehaviour
{
    public Transform wallTile;

    public Sprite wallTop;

    private int edgeWidth = 3;

    private static float wallWidth = 1f;
    private static float scale = 0.1f;
    private static int maxX = (int)(100f / wallWidth);
    private static int maxY = (int)(100f / wallWidth);
    private int[,] world;

    public Transform wallTiles;


    void Start()
    {
        world = new int[maxX, maxY];
        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
                if (IsWall(x, y))
                {
                    Vector3 pos = new Vector3(x * wallWidth, y * wallWidth, 2);
                    Transform t = CreateTile(wallTile, pos, null, wallTiles);
                    t.localScale = new Vector3(wallWidth, wallWidth);
                }
            }
        }
    }

    bool IsWall(float x, float y)
    {
        float noise = Mathf.PerlinNoise(x * scale, y * scale);
        return noise > 0.5;
    }

    int TileAt(int x, int y)
    {
        if (x < 0 || x >= maxX || y < 0 || y >= maxY)
        {
            return 0;
        }
        else
        {
            return world[x, y];
        }
    }

    Transform CreateTile(Transform tile, Vector3 position, Sprite sprite, Transform parent)
    {
        Transform t = Instantiate(tile, position, Quaternion.identity);

        if (sprite != null)
        {
            t.GetComponent<SpriteRenderer>().sprite = sprite;
        }

        t.tag = "Wall";

        if (parent != null)
        {
            t.parent = parent;
        }

        return t;
    }

    Transform CreateExit(Transform tile, Vector3 position, Transform parent, int deltaX, int deltaY)
    {
        Transform t = Instantiate(tile, position, Quaternion.identity);
        t.tag = "Exit";

        t.GetComponent<Exit>().deltaX = deltaX;
        t.GetComponent<Exit>().deltaY = deltaY;

        if (parent != null)
        {
            t.parent = parent;
        }

        return t;
    }
}
