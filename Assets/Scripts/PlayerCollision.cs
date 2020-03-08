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

        if (collisionObject.tag.Contains("Powerup"))
        {
            CollectPowerup(collisionObject);
        }
        else {
            switch (collisionObject.tag)
            {
                case "Enemy":
                    AttackedByEnemy(collisionObject);
                    break;
                case "Exit":
                    MoveToExit(collisionObject);
                    break;
                case "EnemyBullet":
                    HitByBullet(collisionObject);
                    break;
                default:
                    break;
            }
        }

        
    }

    private void CollectPowerup(GameObject powerup)
	{
        switch (powerup.tag)
        {
            case "SpeedPowerup":
                if (GameFlow.Instance.playerSpeed < 50f)
                {
                    GameFlow.Instance.playerSpeed += 10f;
                }
                break;
            case "HealthPowerup":
                health.Increment();
                break;
            case "BulletCountPowerup":
                if (GameFlow.Instance.bulletCount < 5)
                {
                    GameFlow.Instance.bulletCount++;
                }
                break;
        }
        // play sound effect
        SoundMixer.soundeffect.PlayOneShot(SoundMixer.sounds["Powerup"]);

        Destroy(powerup);
    }

    private void AttackedByEnemy(GameObject enemy)
	{
        // get pushed back
        Vector3 enemyToPlayer = (transform.position - enemy.transform.position).normalized;
        transform.position += enemyToPlayer * 3;
        movement.SetStunned();

        TakeDamage(1);
    }

    private void HitByBullet(GameObject enemyBullet)
    {
        TakeDamage(1);
        Destroy(enemyBullet);
    }

    private void TakeDamage(int damage)
    {
        health.Decrement(damage);
        if (health.GetCurrentHP() <= 0)
        {
            PlayerDeath();
        }

        // play hurt sound 
        SoundMixer.soundeffect.PlayOneShot(SoundMixer.sounds["Player_Damage"]);
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
