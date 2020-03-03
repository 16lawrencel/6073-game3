using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    internal Rigidbody2D rigidbody;
    internal Movement movement;
    internal Health health;

    void Awake()
	{
        rigidbody = GetComponent<Rigidbody2D>();
        movement = GetComponent<Movement>();
        health = GetComponent<Health>();
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player collided with " + collision.gameObject);

        GameObject collisionObject = collision.gameObject;

        switch (collisionObject.tag)
        {
            case "Powerup":
                CollectPowerup(collisionObject);
                break;
            case "Enemy":
                AttackedByEnemy(collisionObject);
                break;
            case "Exit":
                MoveToExit(collisionObject);
                break;
            default:
                break;
        }
    }

    private void CollectPowerup(GameObject powerup)
	{
        GetComponent<Movement>().speed *= 2;
        Destroy(powerup);
    }

    private void AttackedByEnemy(GameObject enemy)
	{
        // get pushed back
        Vector3 enemyToPlayer = (transform.position - enemy.transform.position).normalized;
        transform.position += enemyToPlayer * 3;
        movement.SetStunned();

        // deal damage
        health.Decrement(1);
        if (health.GetCurrentHP() <= 0)
		{
            PlayerDeath();
		}
    }

    private void MoveToExit(GameObject exit)
    {
        int deltaX = exit.GetComponent<Exit>().deltaX;
        int deltaY = exit.GetComponent<Exit>().deltaY;

        GameFlow.Instance.MoveRoom(deltaX, deltaY);
    }

    // on death, heal back to max and respawn back to starting point
    private void PlayerDeath()
    {
        health.SetCurrentHP(health.maxHP);
        transform.position = new Vector2(0, 0);
        GameFlow.Instance.RespawnPlayer();
    }
}
