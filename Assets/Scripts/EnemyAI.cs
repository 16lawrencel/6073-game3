using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    internal GameObject player;
    internal Health health;

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
        if (health.GetCurrentHP() <= 0)
        {
            Destroy(gameObject);
        }
    }
}
