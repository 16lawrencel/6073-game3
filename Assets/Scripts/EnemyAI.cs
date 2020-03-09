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
                transform.position + new Vector3((Random.value - 0.5f), (Random.value - 0.5f), GameFlow.Instance.splatHeight - transform.position.z),
                Quaternion.identity);
            GameFlow.Instance.splatHeight -= 0.001f;
            splat.localScale = new Vector3((1 + Random.value)*2, (1 + Random.value)*2, 0);
            splat.eulerAngles = new Vector3(0, 0, Random.value * 360);
            GameObject currentRoom = GameFlow.Instance.GetCurrentRoom();
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
//        Debug.Log(transform.position);
//        Debug.Log(position);
        Debug.Log("EnemyAI is moving at speed " + speed);
        // https://forum.unity.com/threads/make-object-continue-moving-to-destination.134459/
        float distance = Vector2.Distance(transform.position, position);
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime / distance);
        return distance > THRESHOLD;
    }
}
