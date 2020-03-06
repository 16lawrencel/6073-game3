using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// provides a collection of functions which deal with room generation
// with a random seed
public static class RoomHash
{

    public static int MAX_ENEMIES = 4;
    public static int MAX_POWERUPS = 1;

    // num is an identifier for convenient use of GetRandom
    // be sure not to use the same num twice for different functions!
    public static int GetRandom(string seed, int roomX, int roomY, string func, int num)
    {
        // TODO: better delimiter
        string hashString = seed + " " + roomX.ToString() + " " + roomY.ToString() + " " + func + " " + num.ToString();
        return Mathf.Abs(hashString.GetHashCode());
    }

    public static void RandomShuffle<T>(T[] ar, string seed, int roomX, int roomY, string func)
    {
        // Fisher-Yates shuffle
        for (int i = ar.Length - 1; i > 0; i--)
        {
            int j = GetRandom(seed, roomX, roomY, func, i) % (i + 1);
            // swap i, j
            T tmp = ar[i];
            ar[i] = ar[j];
            ar[j] = tmp;
        }
    }


    public static List<Vector2Int> GetEnemyPositions(string seed, int roomX, int roomY)
    {
        int numEnemies = GetRandom(seed, roomX, roomY, "enemy", 0) % (MAX_ENEMIES + 1);

        Vector2Int[] enemyPos = { new Vector2Int(1, 1), new Vector2Int(1, -1), new Vector2Int(-1, 1), new Vector2Int(-1, -1) };

        RandomShuffle(enemyPos, seed, roomX, roomY, "enemy");

        List<Vector2Int> ret = new List<Vector2Int>();
        for (int i = 0; i < numEnemies; i++)
        {
            ret.Add(enemyPos[i]);
        }

        return ret;
    }

    public static List<Vector2Int> GetPowerupPositions(string seed, int roomX, int roomY)
    {
        int numPowerups = GetRandom(seed, roomX, roomY, "powerup", 0) % (MAX_POWERUPS + 1);

        Vector2Int[] powerupPos = { new Vector2Int(1, 1), new Vector2Int(1, -1), new Vector2Int(-1, 1), new Vector2Int(-1, -1) };

        RandomShuffle(powerupPos, seed, roomX, roomY, "powerup");

        List<Vector2Int> ret = new List<Vector2Int>();
        for (int i = 0; i < numPowerups; i++)
        {
            ret.Add(powerupPos[i]);
        }

        return ret;
    }
}
