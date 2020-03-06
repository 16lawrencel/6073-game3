using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationBlock : MonoBehaviour {
    public Transform wallBlock;
    public Transform floorTile;
    public Transform exitTile;

    private int edgeWidth = 3;
    private int maxX = 17;
    private int maxY = 19;
    private int[,] world;

    public Transform bgTiles;
    public Transform wallTiles;
    public Transform exitTiles;
    
    void Start() {
        world = new int[maxX,maxY];
        for (int x = 0; x < maxX; x++) {
            for (int y = 0; y < maxY; y++) {
                world[x, y] = 0;
                if (x >= 2 && x < 16 && y >= 2 && y < 18) {
                    world[x, y] = 1;
                }
                if (x >= 7 && x <= 8 && y >= 18 && y < 20) {
                    world[x, y] = 1;
                }
                if (x >= 7 && x <= 8 && y >= 0 && y < 2) {
                    world[x, y] = 1;
                }
                if (x >= 15 && x <= 17 && y >= 9 && y <= 10) {
                    world[x, y] = 1;
                }
                if (x >= 0 && x <= 2 && y >= 9 && y <= 10) {
                    world[x, y] = 1;
                }
            }
        }
        for (int x = 0; x < maxX; x++) {
            for (int y = 0; y < maxY; y++) {
                Vector3 pos = new Vector3(x * 2 - 22, y * 2 - 12, 0.2f);
                // Open space
                if (TileAt(x, y) == 1) {
                    Transform tf = Instantiate(floorTile, pos + new Vector3(0, 0, 0.1f), Quaternion.identity);
                    tf.parent = bgTiles;
                }
                // Wall
                else if (TileAt(x, y) == 0) {
                    Transform tf = Instantiate(wallBlock, pos + new Vector3(0, 0, 0), Quaternion.identity);
                    tf.parent = wallTiles;
                }
            }
        }
    }

    int TileAt(int x, int y) {
        if (x < 0 || x >= maxX || y < 0 || y >= maxY) {
            return 0;
        }
        else {
            return world[x, y];
        }
    }
}
