using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    private Rigidbody2D rb;
    public Transform sprite;
    
    private float rot = 0;
    // Update is called once per frame

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (rb.velocity.magnitude > 0) {
            rot += Time.deltaTime * 10;
            sprite.eulerAngles = new Vector3(0, 0, Mathf.Sin(rot)*10);
        }

        if (rb.velocity.x < 0) {
            sprite.localScale = new Vector3(-0.1f, 0.1f, 1);
        }
        else {
            sprite.localScale = new Vector3(0.1f, 0.1f, 1);
        }
    }
}
