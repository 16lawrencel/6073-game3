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

    private int ROOM_WIDTH = 800;
    private int ROOM_HEIGHT = 400;
    private int ROOM_GAP = 100;

    public GameObject player;
    public Camera camera;
    public GameObject minimapCamera;
    public GameObject minimapRooms; // namespace to store minimap rooms / textures
    public Sprite pixel;
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
        currentRoomPosition = new Vector2Int(0, 0);
        GameObject startRoom = CreateRoom(currentRoomPosition);
        rooms.Add(currentRoomPosition, startRoom);
	}

    public void MoveRoom(int deltaX, int deltaY)
    {
        GameObject currentRoom = rooms[currentRoomPosition];
        currentRoom.SetActive(false);

        Vector2Int newRoomPosition = currentRoomPosition + (new Vector2Int(deltaX, deltaY));
        GameObject newRoom;

        if (!rooms.ContainsKey(newRoomPosition))
        {
            // create new room
            // TODO: make settings of room a function of the room position
            newRoom = CreateRoom(newRoomPosition);
            rooms.Add(newRoomPosition, newRoom);
        }
        else
        {
            newRoom = rooms[newRoomPosition];
        }

        currentRoomPosition = newRoomPosition;
        newRoom.SetActive(true);

        player.transform.position = new Vector2(-12 * deltaX, -5 * deltaY);
    }

    private GameObject CreateRoom(Vector2Int roomPosition)
    {
        CreateRoomTexture(roomPosition);

        GameObject room = Instantiate(roomPrefab, transform.position, Quaternion.identity);
        room.name = "Room " + roomPosition;

        room.transform.Find("Canvas").GetComponent<Canvas>().worldCamera = camera;
        room.transform.Find("Canvas").Find("RoomText").GetComponent<Text>().text = room.name;
        return room;
    }

    private void CreateRoomTexture(Vector2Int roomPosition)
    {
        GameObject obj = new GameObject("room");
        obj.transform.parent = minimapRooms.transform;
        Vector2 centerPixels = GetRoomCenter(roomPosition);
        Vector2 centerPixelsAdjusted = new Vector2(centerPixels.x / 5 + camera.pixelWidth / 2, centerPixels.y / 5 + camera.pixelHeight / 2); // TODO: make this less hacky
        Vector2 centerWorld = camera.ScreenToWorldPoint(centerPixelsAdjusted);
        obj.transform.localPosition = centerWorld;
        obj.layer = Globals.MINIMAP_LAYER;

        SpriteRenderer renderer = obj.AddComponent<SpriteRenderer>();
        renderer.sprite = pixel;
        renderer.transform.localScale = new Vector2(ROOM_WIDTH, ROOM_HEIGHT);
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
}