using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {
    public float destroyTime = 1;

    private void Start() {
        StartCoroutine(DestroyAfter(destroyTime));
    }

    IEnumerator DestroyAfter(float dt) {
        yield return new WaitForSeconds(dt);
        Destroy(gameObject);
    }
}
