using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    internal Vector3 direction;
    internal float speed;
    internal BoxCollider2D boxCollider;
    Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        boxCollider = GetComponent<BoxCollider2D>();
        // boxCollider.size = transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnBecameInvisible()
    {
        // Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bullet collided with " + collision.gameObject);
        Debug.Log(collision.gameObject.tag);

        GameObject collisionObject = collision.gameObject;

        switch (collisionObject.tag)
        {
            case "Enemy":
                collisionObject.GetComponent<EnemyAI>().TakeDamage(1);
                Destroy(gameObject);
                break;
            case "Wall":
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
