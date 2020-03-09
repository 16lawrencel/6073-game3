using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    internal Vector3 direction;
    internal float speed;
    internal BoxCollider2D boxCollider;
    Camera camera;
    private Vector3 initialpos;
    private float distMult = 1;

    public Transform onDestroy;
    public Transform trail;
    private float count = 0;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        boxCollider = GetComponent<BoxCollider2D>();
        initialpos = transform.position;
        distMult = 1 + Random.value * 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        if ((initialpos - transform.position).magnitude > GameFlow.Instance.bulletRange*distMult) {
            Splat(false);
            Destroy(gameObject);
        }

        count -= Time.deltaTime;
        if (count <= 0) {
            count = 0.01f;
            Transform splat = Instantiate(trail, 
                transform.position + new Vector3((Random.value - 0.5f), (Random.value - 0.5f), GameFlow.Instance.splatHeight - transform.position.z),
                Quaternion.identity);
            GameFlow.Instance.splatHeight -= 0.001f;
            splat.localScale = new Vector3((1 + Random.value)*0.3f, (1 + Random.value)*0.3f, 0);
            splat.eulerAngles = new Vector3(0, 0, Random.value * 360);
            // make splat the child of current room
            GameObject currentRoom = GameFlow.Instance.GetRoom(transform.position);
            splat.transform.parent = currentRoom.transform.Find("Other");
        }
    }

    void OnBecameInvisible()
    {
        // Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //Debug.Log("Bullet collided with " + collision.gameObject);
        //Debug.Log(collision.gameObject.tag);

        GameObject collisionObject = collision.gameObject;

        switch (collisionObject.tag) {
            case "Enemy":
                AttackEnemy(collisionObject);
                Splat(true);
                Destroy(gameObject);
                SoundMixer.soundeffect.PlayOneShot(SoundMixer.sounds["Splash"], 0.6f);
                break;
            case "Wall":
                Splat(true);
                Destroy(gameObject);
                SoundMixer.soundeffect.PlayOneShot(SoundMixer.sounds["Splash"], 0.8f);

                break;
            default:
                break;
        }
    }

    private void AttackEnemy(GameObject enemy)
    {
        Health enemyHealth = enemy.GetComponent<Health>();
        enemyHealth.Decrement(1);
        if (enemyHealth.GetCurrentHP() <= 0)
        {
            Debug.Log("Destroy");
            Destroy(enemy);
        }
    }

    private void Splat(bool shake)
    {
        SpawnOnDestroy();
        if (shake) {
            Camera.main.transform.parent.GetComponent<CameraShake>().Shake(0.3f);
        }
    }

    private void SpawnOnDestroy()
    {
        Transform splat = Instantiate(onDestroy, 
            transform.position + new Vector3((Random.value - 0.5f), (Random.value - 0.5f), GameFlow.Instance.splatHeight - transform.position.z),
            Quaternion.identity);
        splat.localScale = new Vector3(1 + Random.value, 1 + Random.value, 0);
        splat.eulerAngles = new Vector3(0, 0, Random.value * 360);
        GameFlow.Instance.splatHeight -= 0.001f;
        // make splat the child of current room
        GameObject currentRoom = GameFlow.Instance.GetRoom(transform.position);
        splat.transform.parent = currentRoom.transform.Find("Other");
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
