using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour {
    public Transform wallTile;
    public Transform floorTile;

    public Sprite wallTop;
    public Sprite wallLeft;
    public Sprite wallRight;
    public Sprite wallBottom;

    public Sprite bgFloor;
    public Sprite bgUpper;
    public Sprite bgFloorUpWall;
    public Sprite bgFloorDownWall;

    private int edgeWidth = 3;
    private int maxX = 24;
    private int maxY = 14;
    private int[,] world;

    public Transform bgTiles;
    public Transform wallTiles;


    void Start() {
        world = new int[maxX,maxY];
        for (int x = 0; x < maxX; x++) {
            for (int y = 0; y < maxY; y++) {
                world[x, y] = 0;
                if (x >= edgeWidth && x < maxX - edgeWidth && y >= edgeWidth && y < maxY - edgeWidth) {
                    world[x, y] = 1;
                }
            }
        }
        for (int x = 0; x < maxX; x++) {
            for (int y = 0; y < maxY; y++) {
                Vector3 pos = (new Vector3(x * 2 - 22, y * 2 - 12, 2)) + transform.position; // position is relative to our position
                Transform t = null;
                Transform tf = Instantiate(floorTile, pos + new Vector3(0, 0, 1), Quaternion.identity);
                tf.parent = bgTiles;
                tf.tag = "Wall";
                // Open space
                if (TileAt(x, y) == 1) {
                    tf.GetComponent<SpriteRenderer>().sprite = bgFloor;
                    if (TileAt(x - 1, y) == 0) {
                        Transform tf2 = Instantiate(floorTile, pos + new Vector3(0, 0, 0.8f), Quaternion.identity);
                        tf2.GetComponent<SpriteRenderer>().sprite = wallRight;
                        tf2.parent = bgTiles;
                        tf2.tag = "Wall";
                    }
                    if (TileAt(x + 1, y) == 0) {
                        Transform tf2 = Instantiate(floorTile, pos + new Vector3(0, 0, 0.8f), Quaternion.identity);
                        tf2.GetComponent<SpriteRenderer>().sprite = wallLeft;
                        tf2.parent = bgTiles;
                        tf2.tag = "Wall";
                    }
                    if (TileAt(x, y + 1) == 0) {
                        Transform tf2 = Instantiate(floorTile, pos + new Vector3(0, 0, 0.9f), Quaternion.identity);
                        tf2.GetComponent<SpriteRenderer>().sprite = bgFloorUpWall;
                        tf2.parent = bgTiles;
                        tf2.tag = "Wall";
                    }
                }
                // Wall
                else if (TileAt(x, y) == 0) {
                    t = Instantiate(wallTile, pos + new Vector3(0, 0, -1), Quaternion.identity);
                    t.GetComponent<SpriteRenderer>().sprite = bgUpper;
                    t.tag = "Wall";
                    if (TileAt(x, y - 1) == 1) {
                        Transform tf2 = Instantiate(floorTile, pos + new Vector3(0, 0, -1.1f), Quaternion.identity);
                        tf2.GetComponent<SpriteRenderer>().sprite = wallTop;
                        tf2.parent = bgTiles;
                        tf2.tag = "Wall";
                    }
                    if (TileAt(x, y + 1) == 1) {
                        Transform tf2 = Instantiate(floorTile, pos + new Vector3(0, 0, -1.1f), Quaternion.identity);
                        tf2.GetComponent<SpriteRenderer>().sprite = bgFloorDownWall;
                        tf2.parent = bgTiles;
                        tf2.tag = "Wall";
                    }
                }
                if (t != null) {
                    t.parent = wallTiles;
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
