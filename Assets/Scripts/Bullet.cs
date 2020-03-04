using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    internal Vector3 direction;
    internal float speed;
    internal BoxCollider2D boxCollider;
    Camera camera;

    public Transform onDestroy;

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

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        Debug.Log("Bullet collided with " + collision.gameObject);
//
//        EnemyAI enemyAI = (EnemyAI) collision.gameObject.GetComponent<EnemyAI>();
//        if (enemyAI != null)
//        {
//            // call enemy health decrement
//            Destroy(gameObject);
//            Destroy(enemyAI.gameObject);
//        }
//    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Bullet collided with " + collision.gameObject);
        Debug.Log(collision.gameObject.tag);

        GameObject collisionObject = collision.gameObject;

        switch (collisionObject.tag) {
            case "Enemy":
                AttackEnemy(collisionObject);
                Destroy(gameObject);
                SoundMixer.soundeffect.PlayOneShot(SoundMixer.sounds["Splash"], 0.6f);
                break;
            case "Wall":
                SpawnOnDestroy();

                Camera.main.GetComponent<CameraShake>().Shake(1);
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
            Destroy(enemy);
        }
    }

    private void SpawnOnDestroy()
    {
        Transform splat = Instantiate(onDestroy,
transform.position + new Vector3((Random.value - 0.5f), (Random.value - 0.5f), 0),
Quaternion.identity);
        splat.localScale = new Vector3(1 + Random.value, 1 + Random.value, 0);
        splat.eulerAngles = new Vector3(0, 0, Random.value * 360);
        // make splat the child of current room
        GameObject currentRoom = GameFlow.Instance.GetCurrentRoom();
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
