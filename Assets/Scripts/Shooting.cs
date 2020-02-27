using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    internal Rotation rotator;

    // Start is called before the first frame update
    void Start()
    {
        rotator = GetComponent<Rotation>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rotator.FaceToward(mousePosition);
    }
}
