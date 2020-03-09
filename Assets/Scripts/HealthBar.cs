using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Health health;
    internal GameObject bar;
    internal GameObject barSprite;
    internal GameObject background;

    void Awake()
    {
        health = GetComponentInParent<Health>();
        bar = transform.Find("Bar").gameObject;
        barSprite = bar.transform.Find("BarSprite").gameObject;
        background = transform.Find("Background").gameObject;
    }

    void Start()
    {
        bar.transform.localScale = new Vector3(1f, 1f, 1);
        bar.transform.position = new Vector3(bar.transform.position.x, bar.transform.position.y, -1f);
    }

    void Update()
    {
        float percentage = health.Percentage();
        bool enabled = (percentage != 1f);

        background.GetComponent<SpriteRenderer>().enabled = enabled;
        barSprite.GetComponent<SpriteRenderer>().enabled = enabled;

        bar.transform.localScale = new Vector3(percentage, 1f, 1);
    }

    // used by InfoMenu to hook up healthbar to original unit's health
    public void setHealth(Health newHealth)
    {
        health = newHealth;
    }

}
