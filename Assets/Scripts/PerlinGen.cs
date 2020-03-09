using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinGen : MonoBehaviour
{
    public GameObject speedPowerupPrefab;
    public GameObject healthPowerupPrefab;
    public GameObject bulletCountPowerupPrefab;
    public GameObject enemyPrefab;

    public Transform wallTile;
    public Transform floorTile;

    private int edgeWidth = 3;

    private static float wallWidth = 2f;
    private static int maxX = 25;
    private static int maxY = 25;

    private static float offset = 1000f;
    private static float scale = 0.05f;
    private static float wallThreshold = 0.6f; // must be > threshold to be wall
    private static float objectThreshold = 0.3f; // must be < threshold to have chance to spawn powerup / enemy

    private static float enemyP = 0.05f; // probability of enemy
    private static float speedPowerupP = 0.005f; // probability of speed powerup
    private static float healthPowerupP = 0.005f; // probability of health powerup
    private static float bulletPowerupP = 0.005f; // probability of bullet powerup

    private int[,] world;

    public Transform wallTiles;
    public Transform floorTiles;
    public Transform enemies; // TODO: make enemies "hop" rooms when they move away
    public Transform powerups;


    void Start()
    {
        world = new int[maxX, maxY];
        for (int x = -maxX / 2; x <= maxX / 2; x++)
        {
            for (int y = -maxY / 2; y <= maxY / 2; y++)
            {
                Vector3 pos = new Vector3(transform.position.x + x * wallWidth, transform.position.y + y * wallWidth, 2);
                Vector3 objectPos = pos - new Vector3(0, 0, 4);

                float noise = Mathf.PerlinNoise(pos.x * scale + offset, pos.y * scale + offset);

                if (noise > wallThreshold)
                {
                    Transform t = CreateTile(wallTile, pos, null, wallTiles);
                }
                else
                {
                    Transform t = CreateTile(floorTile, pos, null, floorTiles);
                }

                if (noise < objectThreshold)
                {
                    float rand = Random.Range(0f, 1f);
                    if (rand < enemyP)
                    {
                        GameObject enemy = Instantiate(enemyPrefab, objectPos, Quaternion.identity);
                        enemy.transform.parent = enemies;
                    }
                    else if (rand < enemyP + speedPowerupP)
                    {
                        GameObject powerup = Instantiate(speedPowerupPrefab, objectPos, Quaternion.identity);
                        powerup.transform.parent = powerups;
                    }
                    else if (rand < enemyP + speedPowerupP + healthPowerupP)
                    {
                        GameObject powerup = Instantiate(healthPowerupPrefab, objectPos, Quaternion.identity);
                        powerup.transform.parent = powerups;
                    }
                    else if (rand < enemyP + speedPowerupP + healthPowerupP + bulletPowerupP)
                    {
                        GameObject powerup = Instantiate(bulletCountPowerupPrefab, objectPos, Quaternion.identity);
                        powerup.transform.parent = powerups;
                    }
                }
            }
        }
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
