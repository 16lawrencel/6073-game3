using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFlow : MonoBehaviour
{
    protected static GameFlow instance;
    public static GameFlow Instance
    {
        get
        {
            if (instance != null)
                return instance;
            instance = FindObjectOfType<GameFlow>();
            return instance;
        }
    }

    public string seed;

    private static float wallWidth = 2f;
    private static float ROOM_WIDTH = wallWidth * 25;
    private static float ROOM_HEIGHT = wallWidth * 25;

    private static float ROOM_GAP = 100;

    public float playerSpeed = 10f;
    public float bulletRange = 2;
    public int bulletCount = 1;

    public float splatHeight = 1;

    public bool isBoss = false;

    public GameObject player;
    public Camera camera;

    // namespaces to organize rooms / minimaps
    public GameObject namespaceRooms;

    private Color unoccupiedColor = new Color(255, 255, 255);
    private Color occupiedColor = new Color(0, 0, 0);

    public Dictionary<Vector2Int, GameObject> rooms;

    public GameObject roomPrefab;
    public Vector2Int currentRoomPosition;

    void Awake()
    {
        // QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        rooms = new Dictionary<Vector2Int, GameObject>();
    }

    void Start()
    {
        //currentRoomPosition = new Vector2Int(0, 0);
        //CreateRoom(currentRoomPosition);
    }

    void Update()
    {
        if (!isBoss)
        {
            Vector3 cameraPos = camera.transform.position;
            Vector3 playerPos = player.transform.position;

            float cameraHeight = 2f * camera.orthographicSize;
            float cameraWidth = cameraHeight * camera.aspect;

            float worldMinX = cameraPos.x - cameraWidth - ROOM_WIDTH;
            float worldMaxX = cameraPos.x + cameraWidth + ROOM_WIDTH;
            float worldMinY = cameraPos.y - cameraHeight - ROOM_HEIGHT;
            float worldMaxY = cameraPos.y + cameraHeight + ROOM_HEIGHT;

            int minX = (int)(worldMinX / ROOM_WIDTH);
            int maxX = (int)(worldMaxX / ROOM_WIDTH);
            int minY = (int)(worldMinY / ROOM_HEIGHT);
            int maxY = (int)(worldMaxY / ROOM_HEIGHT);

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    CreateRoomIfNotExist(x, y);
                }
            }

            for (int x = minX - 1; x <= maxX + 1; x++)
                for (int y = minY - 1; y <= maxY + 1; y++)
                    if (x < minX || x > maxX || y < minY || y > maxY)
                    {
                        DeactivateRoom(x, y);
                    }
        }
    }


    // creates a room at (x, y) if it doesn't exist
    // then activates the room (whether it was created or not)
    private void CreateRoomIfNotExist(int x, int y)
    {
        Vector2Int roomPos = new Vector2Int(x, y);

        if (!rooms.ContainsKey(roomPos))
        {
            GameObject newRoom = CreateRoom(roomPos);
        }

        rooms[roomPos].SetActive(true);
    }

    // deactivates the room at (x, y) if it exists
    private void DeactivateRoom(int x, int y)
    {
        Vector2Int roomPos = new Vector2Int(x, y);

        if (rooms.ContainsKey(roomPos))
        {
            rooms[roomPos].SetActive(false);
        }
    }

    private GameObject CreateRoom(Vector2Int roomPosition)
    {
        Vector3 worldRoomPosition = transform.position + (new Vector3(roomPosition.x * ROOM_WIDTH, roomPosition.y * ROOM_HEIGHT));

        GameObject room = Instantiate(roomPrefab, worldRoomPosition, Quaternion.identity);
        room.name = "Room " + roomPosition;
        room.transform.parent = namespaceRooms.transform;

        rooms.Add(roomPosition, room);

        return room;
    }

    public Vector2Int GetRoomPosition(float x, float y)
    {
        int roomX = Mathf.FloorToInt((x + ROOM_WIDTH / 2) / ROOM_WIDTH);
        int roomY = Mathf.FloorToInt((y + ROOM_HEIGHT / 2) / ROOM_HEIGHT);
        return new Vector2Int(roomX, roomY);
    }

    public GameObject GetRoom(Vector3 position)
    {
        float x = position.x;
        float y = position.y;

        Vector2Int roomPosition = GetRoomPosition(x, y);
        if (rooms.ContainsKey(roomPosition))
        {
            return rooms[roomPosition];
        }
        else
        {
            return null;
        }
    }

    // respawn player back to starting room
    // callback function, currently doesn't do anything
    public void RespawnPlayer()
    {
    }

    /*
    // set all persisting objects to be destroyed when scene switches again
    // (to either win or lose screen)
    public void DestroyObjectsOnLoad()
    {
        Persist[] persistList = FindObjectsOfType(typeof(Persist)) as Persist[];
        foreach (Persist persist in persistList)
        {
            persist.DestroyOnLoad();
        }
    }
    */

    public void SetBoss()
    {
        isBoss = true;

        foreach (KeyValuePair<Vector2Int, GameObject> pair in rooms)
        {
            GameObject room = pair.Value;
            Destroy(room);
        }

        rooms.Clear();

        CreateRoomIfNotExist(0, 0);

        // spawn boss


        player.transform.position = new Vector2(0, 0);
    }
}