using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinGen : MonoBehaviour
{
    public Transform wallTile;
    public Transform floorTile;

    private int edgeWidth = 3;

    private static float wallWidth = 2f;
    private static float scale = 0.05f;
    private static int maxX = 25;
    private static int maxY = 25;

    private int[,] world;

    public Transform wallTiles;
    public Transform floorTiles;


    void Start()
    {
        world = new int[maxX, maxY];
        for (int x = -maxX / 2; x <= maxX / 2; x++)
        {
            for (int y = -maxY / 2; y <= maxY / 2; y++)
            {
                Vector3 pos = new Vector3(transform.position.x + x * wallWidth, transform.position.y + y * wallWidth, 2);
                if (IsWall(pos.x, pos.y))
                {
                    Transform t = CreateTile(wallTile, pos, null, wallTiles);
                }
                else
                {
                    Transform t = CreateTile(floorTile, pos, null, floorTiles);
                }
            }
        }
    }

    bool IsWall(float x, float y)
    {
        float noise = Mathf.PerlinNoise(x * scale, y * scale);
        return noise > 0.6;
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
