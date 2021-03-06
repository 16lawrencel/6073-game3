﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    internal GameObject player;
    internal Health health;
    internal float speed;
    public Transform onDeath;
    public float THRESHOLD = .01f;

    internal GameObject currentRoom;

    // manually added Start method to be called from subclasses
    protected void Start()
    {
        player = GameObject.Find("Player");
        health = gameObject.GetComponent<Health>();

        currentRoom = transform.parent.parent.parent.gameObject;
    }

    // manually added Update method to be called from subclasses
    protected void Update()
    {
        // "hop" to its current room
        // to prevent enemies which strayed too far from their
        // starting room to get despawned

        GameObject room = GameFlow.Instance.GetRoom(transform.position);

        // careful to test for reference equality here
        if (room != currentRoom && !GameFlow.Instance.isBoss)
        {
            currentRoom = room;
            transform.parent = currentRoom.transform.Find("Generation").Find("Enemies");
        }
    }

    public void TakeDamage(int damage)
    {
        health.Decrement(damage);
        if (health.GetCurrentHP() <= 0) {
            Transform splat = Instantiate(onDeath, 
                transform.position + new Vector3((Random.value - 0.5f), (Random.value - 0.5f), GameFlow.Instance.splatHeight - transform.position.z),
                Quaternion.identity);
            //GameFlow.Instance.splatHeight -= 0.001f;
            splat.localScale = new Vector3((1 + Random.value)*2, (1 + Random.value)*2, 0);
            splat.eulerAngles = new Vector3(0, 0, Random.value * 360);
            GameObject currentRoom = GameFlow.Instance.GetRoom(transform.position);
            splat.transform.parent = currentRoom.transform.Find("Other");
            Camera.main.transform.parent.GetComponent<CameraShake>().Shake(1f);
            Destroy(gameObject);
        }
    }

    protected void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    protected bool MoveTowards(Vector2 position)
    {
        return MoveTowards(new Vector3(position.x, position.y, transform.position.z));
    }

    protected bool MoveTowards(Vector3 position) {
        position.z = transform.position.z;
        // https://forum.unity.com/threads/make-object-continue-moving-to-destination.134459/
        float distance = Vector2.Distance(transform.position, position);
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime / distance);
        return distance > THRESHOLD;
    }
}
