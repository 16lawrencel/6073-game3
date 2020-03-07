using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : EnemyAI
{
    [SerializeField]
    GameObject bulletPrefab;
    internal float movementTimer;
    internal float shootingTimer;
    public Vector2 destination;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        SetSpeed(Globals.ENEMY_SHOOTER_SPEED);
        movementTimer = Globals.ENEMY_SHOOTER_NEW_DESTINATION_SECONDS;
        shootingTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        // movement
        movementTimer += Time.deltaTime;
        if (movementTimer >= Globals.ENEMY_SHOOTER_NEW_DESTINATION_SECONDS)
        {
            SetNewDestination();
            movementTimer = 0;
        }
        MoveTowards(destination);

        // shooting
        shootingTimer += Time.deltaTime;
        if (shootingTimer >= Globals.ENEMY_SHOOTER_NEW_BULLET_SECONDS)
        {
            ShootPlayer();
            shootingTimer = 0;
        }
    }

    void SetNewDestination()
    {
        float curX = gameObject.transform.position.x;
        float curY = gameObject.transform.position.y;
        destination = new Vector2(curX + Random.Range(-10f, 10f), curY + Random.Range(-10f, 10f));
    }

    void ShootPlayer()
    {
        if (player == null)
            return;
        Debug.Log("EnemyAI is shooting");
        GameObject bulletObj = Instantiate(bulletPrefab, gameObject.transform.position + new Vector3(0, 0, 0.01f), Quaternion.identity);
        BulletEnemy bullet = (BulletEnemy) bulletObj.GetComponent<BulletEnemy>();
        bullet.setDirection(player.transform.position - gameObject.transform.position);
        bullet.setSpeed(Globals.ENEMY_SHOOTER_BULLET_SPEED);
    }
}
