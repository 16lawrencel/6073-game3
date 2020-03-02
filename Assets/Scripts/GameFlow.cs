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

    public Transform player;
    public Transform camera;
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
        GameObject room = Instantiate(roomPrefab, transform.position, Quaternion.identity);
        room.name = "Room " + roomPosition;

        room.transform.Find("Canvas").GetComponent<Canvas>().worldCamera = camera.GetComponent<Camera>();
        room.transform.Find("Canvas").Find("RoomText").GetComponent<Text>().text = room.name;
        return room;
    }
}