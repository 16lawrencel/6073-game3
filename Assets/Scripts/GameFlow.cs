using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public List<GameObject> rooms;

    public GameObject roomPrefab;

    void Awake()
    {
        // QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        rooms = new List<GameObject>();
    }

    void Start()
	{
        GameObject newRoom = Instantiate(roomPrefab, transform.position, Quaternion.identity);
        rooms.Add(newRoom);
	}
}