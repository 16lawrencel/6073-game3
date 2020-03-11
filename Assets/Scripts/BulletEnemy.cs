using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    internal Vector3 direction;
    internal float speed;
    internal BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (!GameFlow.Instance.isBoss)
        {
            GameObject currentRoom = GameFlow.Instance.GetRoom(transform.position);
            transform.parent = currentRoom.transform.Find("Other");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bullet collided with " + collision.gameObject);
        //Debug.Log(collision.gameObject.tag);

        GameObject collisionObject = collision.gameObject;

        switch (collisionObject.tag)
        {
            case "Wall":
                Destroy(gameObject);
                break;
            case "Player":
                collisionObject.GetComponent<PlayerCollision>().TakeDamage(1);
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

    public void setDirection(Vector3 direction)
    {
        this.direction = new Vector3(direction.x, direction.y, 0).normalized;
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }
}
