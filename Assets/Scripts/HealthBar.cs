using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Health health;
    internal Transform bar;

    void Awake()
    {
        health = GetComponentInParent<Health>();
    }

    void Start()
    {
        bar = transform.Find("Bar");
        bar.localScale = new Vector2(1f, 1f);
    }

    void Update()
    {
        float percentage = health.Percentage();
        bool enabled = (percentage != 1f);

        transform.Find("Background").GetComponent<SpriteRenderer>().enabled = enabled;
        transform.Find("Bar").Find("BarSprite").GetComponent<SpriteRenderer>().enabled = enabled;

        bar.localScale = new Vector2(percentage, 1f);
    }

    // used by InfoMenu to hook up healthbar to original unit's health
    public void setHealth(Health newHealth)
    {
        health = newHealth;
    }

}
