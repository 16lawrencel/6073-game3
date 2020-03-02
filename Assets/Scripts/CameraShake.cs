using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float shake = 0f;

    public void Shake(float sh) {
        shake = sh;
    }

    void LateUpdate() {

        if (shake > 0) {
            transform.localPosition = Random.insideUnitSphere * shake;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
            shake -= Time.unscaledDeltaTime * 20;
        }
        else {
            transform.localPosition = new Vector3(0, 0, 0);
            shake = 0.0f;
        }
    }
}
