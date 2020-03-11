using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public GameObject bulletPrefab;

    private float cooldown = 3f;
    private float currentCooldown;

    private List<float> shootTimes;

    void Start()
    {
        currentCooldown = cooldown;
        shootTimes = new List<float>();
    }

    void Update()
    {
        CheckShoot();
        CheckCooldown();
    }

    private void CheckShoot()
    {
        for (int i = shootTimes.Count-1; i >= 0; i--)
        {
            shootTimes[i] -= Time.deltaTime;
            if (shootTimes[i] < 0)
            {
                Shoot();
                shootTimes.RemoveAt(i);
            }
        }
    }

    private void Shoot()
    {
        GameObject player = GameFlow.Instance.player;
        GameObject bulletObj = Instantiate(bulletPrefab, gameObject.transform.position + new Vector3(0, 0, 0.01f), Quaternion.identity);
        BulletEnemy bullet = (BulletEnemy)bulletObj.GetComponent<BulletEnemy>();

        Vector2 dirVec = player.transform.position - gameObject.transform.position;
        Vector2 normVec = new Vector2(dirVec.y, -dirVec.x);
        Vector2 randomVec = normVec * Random.Range(-0.3f, 0.3f);
        Vector2 totalVec = dirVec + randomVec;
        bullet.setDirection(totalVec);

        bullet.setSpeed(Globals.ENEMY_SHOOTER_BULLET_SPEED);
    }

    private void CheckCooldown()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown < 0)
        {
            currentCooldown = cooldown;
            shootTimes = new List<float> { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1f };
        }
    }
}
