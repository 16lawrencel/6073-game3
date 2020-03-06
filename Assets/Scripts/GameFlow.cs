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

    private int ROOM_WIDTH = 800;
    private int ROOM_HEIGHT = 400;
    private int ROOM_GAP = 100;

    public float playerSpeed = 15f;

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
        currentRoomPosition = new Vector2Int(0, 0);
        CreateRoom(currentRoomPosition);
        SetNewRoom(currentRoomPosition);
	}

    public void MoveRoom(int deltaX, int deltaY)
    {
        Vector2Int newRoomPosition = currentRoomPosition + (new Vector2Int(deltaX, deltaY));
        GameObject newRoom;

        if (!rooms.ContainsKey(newRoomPosition))
        {
            newRoom = CreateRoom(newRoomPosition);
        }
        else
        {
            newRoom = rooms[newRoomPosition];
        }

        SetNewRoom(newRoomPosition);

        player.transform.position = new Vector2(-8 * deltaX - 5, -8 * deltaY + 5);
    }

    private void SetNewRoom(Vector2Int newRoomPosition)
    {
        GameObject currentRoom = rooms[currentRoomPosition];
        GameObject currentMinimapRoom = minimapRooms[currentRoomPosition];
        currentRoom.SetActive(false);
        currentMinimapRoom.GetComponent<SpriteRenderer>().color = unoccupiedColor;

        GameObject newRoom = rooms[newRoomPosition];
        GameObject newMinimapRoom = minimapRooms[newRoomPosition];
        newRoom.SetActive(true);
        newMinimapRoom.GetComponent<SpriteRenderer>().color = occupiedColor;

        Vector3 pos = newMinimapRoom.transform.position;
        minimapCamera.gameObject.transform.position = new Vector3(pos.x, pos.y, -100);

        currentRoomPosition = newRoomPosition;
    }


    private GameObject CreateRoom(Vector2Int roomPosition)
    {
        CreateMinimapRoom(roomPosition);

        GameObject room = Instantiate(roomPrefab, transform.position, Quaternion.identity);
        room.name = "Room " + roomPosition;
        room.transform.parent = namespaceRooms.transform;

        room.transform.Find("Canvas").GetComponent<Canvas>().worldCamera = camera;
        room.transform.Find("Canvas").Find("RoomText").GetComponent<Text>().text = room.name;

        room.GetComponent<Room>().seed = seed;
        room.GetComponent<Room>().roomX = roomPosition.x;
        room.GetComponent<Room>().roomY = roomPosition.y;

        rooms.Add(roomPosition, room);

        return room;
    }

    private void CreateMinimapRoom(Vector2Int roomPosition)
    {
        GameObject minimapRoom = new GameObject("Minimap Room " + roomPosition);
        minimapRoom.transform.parent = namespaceMinimapRooms.transform;
        Vector2 centerPixels = GetRoomCenter(roomPosition);
        Vector2 centerPixelsAdjusted = new Vector2(centerPixels.x / 5 + camera.pixelWidth / 2, centerPixels.y / 5 + camera.pixelHeight / 2); // TODO: make this less hacky
        Vector2 centerWorld = camera.ScreenToWorldPoint(centerPixelsAdjusted);
        minimapRoom.transform.localPosition = centerWorld;
        minimapRoom.layer = Globals.MINIMAP_LAYER;

        SpriteRenderer renderer = minimapRoom.AddComponent<SpriteRenderer>();
        renderer.sprite = pixel;
        renderer.transform.localScale = new Vector2(ROOM_WIDTH, ROOM_HEIGHT);

        minimapRooms.Add(roomPosition, minimapRoom);
    }

    // returns the center of the room in minimap pixel coordinates given its
    // position in integer coordinates
    private Vector2 GetRoomCenter(Vector2Int roomPosition)
    {
        return new Vector3(roomPosition.x * (ROOM_WIDTH + ROOM_GAP), roomPosition.y * (ROOM_HEIGHT + ROOM_GAP), -10);
    }

    public GameObject GetCurrentRoom()
    {
        return rooms[currentRoomPosition];
    }

    // respawn player back to starting room
    public void RespawnPlayer()
    {
        SetNewRoom(new Vector2Int(0, 0));
    }
}