using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    internal GameObject player;
    internal Health health;
    internal float speed;
    public Transform onDeath;
    public float THRESHOLD = .01f;

    // manually added Start method to be called from subclasses
    protected void Start()
    {
        player = GameObject.Find("Player");
        health = gameObject.GetComponent<Health>();
    }

    // manually added Update method to be called from subclasses
    protected void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        health.Decrement(damage);
        if (health.GetCurrentHP() <= 0) {
            Transform splat = Instantiate(onDeath, 
                transform.position + new Vector3((Random.value - 0.5f), (Random.value - 0.5f), 0),
                Quaternion.identity);
            splat.localScale = new Vector3(1 + Random.value, 1 + Random.value, 0);
            splat.eulerAngles = new Vector3(0, 0, Random.value * 360);
            GameObject currentRoom = GameFlow.Instance.GetCurrentRoom();
            splat.transform.parent = currentRoom.transform.Find("Other");
            Destroy(gameObject);
        }
    }

    protected void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    protected bool MoveTowards(Vector2 position)
    {
        return MoveTowards(new Vector3(position.x, position.y, gameObject.transform.position.z));
    }

    protected bool MoveTowards(Vector3 position)
    {
        Debug.Log("EnemyAI is moving at speed " + speed);
        // https://forum.unity.com/threads/make-object-continue-moving-to-destination.134459/
        float distance = Vector2.Distance(transform.position, position);
        transform.position = Vector2.Lerp(transform.position, position, speed * Time.deltaTime / distance);
        return distance > THRESHOLD;
    }
}
