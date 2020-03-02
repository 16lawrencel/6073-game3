using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour {
    public Transform wallTile;
    public Transform floorTile;
    public Transform exitTile;

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
    public Transform exitTiles;


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

                Transform tf = CreateTile(floorTile, pos + new Vector3(0, 0, 1), null, bgTiles);

                // Open space
                if (TileAt(x, y) == 1) {
                    tf.GetComponent<SpriteRenderer>().sprite = bgFloor;


                    if (TileAt(x - 1, y) == 0) {
                        if (y == (int)(maxY / 2))
                        {
                            CreateExit(exitTile, pos + new Vector3(0, 0, 0.8f), exitTiles, -1, 0);
                        }
                        else
                        {
                            CreateTile(floorTile, pos + new Vector3(0, 0, 0.8f), wallRight, bgTiles);
                        }
                    }

                    if (TileAt(x + 1, y) == 0) {
                        if (y == (int)(maxY / 2))
                        {
                            CreateExit(exitTile, pos + new Vector3(0, 0, 0.8f), exitTiles, 1, 0);
                        }
                        else
                        {
                            CreateTile(floorTile, pos + new Vector3(0, 0, 0.8f), wallLeft, bgTiles);
                        }
                    }

                    if (TileAt(x, y + 1) == 0) {
                        if (x == (int)(maxX / 2)) {
                            CreateExit(exitTile, pos + new Vector3(0, 0, 0.9f), exitTiles, 0, 1);
                        } else {
                            CreateTile(floorTile, pos + new Vector3(0, 0, 0.9f), bgFloorUpWall, bgTiles);
                        }
                    }
                }

                // Wall
                else if (TileAt(x, y) == 0) {
                    CreateTile(wallTile, pos + new Vector3(0, 0, -1), bgUpper, wallTiles);

                    if (TileAt(x, y - 1) == 1) {
                        CreateTile(floorTile, pos + new Vector3(0, 0, -1.1f), wallTop, bgTiles);
                    }

                    if (TileAt(x, y + 1) == 1) {
                        if (x == (int)(maxX / 2))
                        {
                            CreateExit(exitTile, pos + new Vector3(0, 0, -1.1f), exitTiles, 0, -1);
                        }
                        else
                        {
                            CreateTile(floorTile, pos + new Vector3(0, 0, -1.1f), bgFloorDownWall, bgTiles);
                        }
                    }
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
