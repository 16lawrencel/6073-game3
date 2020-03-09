using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RoomHash;

public class Room : MonoBehaviour
{
    public GameObject powerupPrefab;
    public GameObject healthPowerupPrefab;
    public GameObject bulletCountPowerupPrefab;
    public GameObject enemyPrefab;

    public string seed;
    public int roomX;
    public int roomY;

    // Start is called before the first frame update
    void Start()
    {
        List<Vector2Int> enemyPos = RoomHash.GetEnemyPositions(seed, roomX, roomY);
        List<Vector2Int> powerupPos = RoomHash.GetPowerupPositions(seed, roomX, roomY);
        List<Vector2Int> healthPowerupPos = RoomHash.GetPowerupPositions("health", roomX, roomY);
        List<Vector2Int> bulletCountPowerupPos = RoomHash.GetPowerupPositions("bulletCount", roomX, roomY);

        foreach (Vector2Int pos in enemyPos)
        {
            GameObject enemy = Instantiate(enemyPrefab, new Vector3(13 * pos.x, 5 * pos.y, -1), Quaternion.identity);
            enemy.transform.parent = transform;
            enemy.tag = "Enemy"; // TODO: WHY IS THIS NECESSARY (enemy spawns initially as a powerup for some reason)
        }

        foreach (Vector2Int pos in powerupPos)
        {
            GameObject powerup = Instantiate(powerupPrefab, new Vector3(3 * pos.x, 2 * pos.y, -1), Quaternion.identity);
            powerup.transform.parent = transform;
        }

        foreach (Vector2Int pos in healthPowerupPos)
        {
            GameObject healthPowerup = Instantiate(healthPowerupPrefab, new Vector3(3 * pos.x, 2 * pos.y, -1), Quaternion.identity);
            healthPowerup.transform.parent = transform;
        }

        foreach (Vector2Int pos in bulletCountPowerupPos)
        {
            GameObject bulletCountPowerup = Instantiate(bulletCountPowerupPrefab, new Vector3(3 * pos.x, 2 * pos.y, -1), Quaternion.identity);
            bulletCountPowerup.transform.parent = transform;
        }
    }
}
