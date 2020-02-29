using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public float speed = 0.1f;
    public float THRESHOLD = .01f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        MoveTo(player.transform.position, speed);
    }

    public bool MoveTo(Vector3 position, float speed)
    {
        // https://forum.unity.com/threads/make-object-continue-moving-to-destination.134459/
        float distance = Vector2.Distance(transform.position, position);
        transform.position = Vector2.Lerp(transform.position, position, speed * Time.deltaTime / distance);
        return distance > THRESHOLD;
    }
}
