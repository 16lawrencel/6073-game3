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

    public GameObject player;
    public Camera camera;
    public Camera minimapCamera;

    // namespaces to organize rooms / minimaps
    private GameObject namespaceRooms;
    private GameObject namespaceMinimapRooms;

    private Color unoccupiedColor = new Color(255, 255, 255);
    private Color occupiedColor = new Color(0, 0, 0);

    public Sprite pixel;
    public Dictionary<Vector2Int, GameObject> rooms;
    public Dictionary<Vector2Int, GameObject> minimapRooms;

    public GameObject roomPrefab;
    public Vector2Int currentRoomPosition;

    void Awake()
    {
        // QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        rooms = new Dictionary<Vector2Int, GameObject>();
        minimapRooms = new Dictionary<Vector2Int, GameObject>();

        namespaceRooms = new GameObject("Rooms");
        namespaceMinimapRooms = new GameObject("Minimap Rooms");
    }

    void Start()
    {
        //currentRoomPosition = new Vector2Int(0, 0);
        //CreateRoom(currentRoomPosition);
    }

    void Update()
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

        for (int x = minX-1; x <= maxX+1; x++)
            for (int y = minY-1; y <= maxY+1; y++)
                if (x < minX || x > maxX || y < minY || y > maxY)
                {
                    DeactivateRoom(x, y);
                }
    }


    // creates a room at (x, y) if it doesn't exist
    // then activates the room (whether it was created or not)
    private void CreateRoomIfNotExist(int x, int y)
    {
        Vector2Int roomPos = new Vector2Int(x, y);

        if (!rooms.ContainsKey(roomPos))
        {
            Debug.Log("Generating room");
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

    public void MoveRoom(int deltaX, int deltaY)
    {
        Debug.Log("ERROR");
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
    public void RespawnPlayer()
    {
        Debug.Log("ERROR");
    }
}