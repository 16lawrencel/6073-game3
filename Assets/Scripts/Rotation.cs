using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    internal GameObject sprite;

    internal float THRESHOLD = 0.01f;

    void Awake()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "Sprite")
            {
                sprite = child.gameObject;
                break;
            }
        }
    }

    public void FaceToward(Vector3 position)
    {
        // https://answers.unity.com/questions/654222/make-sprite-look-at-vector2-in-unity-2d-1.html
        Vector3 dir = position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        sprite.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
