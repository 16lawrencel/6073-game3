using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RoomHash;

public class Room : MonoBehaviour
{
    public GameObject powerupPrefab;
    public GameObject enemyPrefab;

    public string seed;
    public int roomX;
    public int roomY;

    // Start is called before the first frame update
    void Start()
    {
        List<Vector2Int> enemyPos = RoomHash.GetEnemyPositions(seed, roomX, roomY);
        List<Vector2Int> powerupPos = RoomHash.GetPowerupPositions(seed, roomX, roomY);

        foreach (Vector2Int pos in enemyPos)
        {
            GameObject enemy = Instantiate(enemyPrefab, new Vector2(13 * pos.x, 5 * pos.y), Quaternion.identity);
            enemy.transform.parent = transform;
        }

        foreach (Vector2Int pos in powerupPos)
        {
            GameObject powerup = Instantiate(powerupPrefab, new Vector2(3 * pos.x, 2 * pos.y), Quaternion.identity);
            powerup.transform.parent = transform;
        }
    }
}
