using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHP = 1;
    internal int currentHP;

    void Awake()
    {
        currentHP = maxHP;
    }

    /// Increment the HP of the entity.
    public void Increment(int by = 1)
    {
        currentHP = Mathf.Clamp(currentHP + by, 0, maxHP);
    }

    /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
    /// current HP reaches 0.
    public void Decrement(int by)
    {
        currentHP = Mathf.Clamp(currentHP - by, 0, maxHP);
    }

    public int GetCurrentHP()
    {
        return currentHP;
    }

    public void SetCurrentHP(int newHP)
    {
        currentHP = newHP;
    }

    public float Percentage()
    {
        return (float)(currentHP) / maxHP;
    }

    public override string ToString()
    {
        return String.Format("{0}/{1}", currentHP, maxHP);
    }
}