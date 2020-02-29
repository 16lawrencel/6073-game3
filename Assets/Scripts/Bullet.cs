using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    internal Vector3 direction;
    internal float speed;
    Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void setDirection(Vector3 direction) {
        direction.z = 0;
        this.direction = direction.normalized;
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }
}
