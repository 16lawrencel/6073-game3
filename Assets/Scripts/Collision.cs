using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("collision");
        GameObject gameObject = col.gameObject;

        switch (gameObject.tag)
        {
            case "Powerup":
                GetComponent<Movement>().speed *= 2;
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
