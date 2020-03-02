using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;
    Camera camera;

    internal Rotation rotator;
    public Transform gun;
    public float cooldown = 0.5f; // number of seconds of cooldown
    internal float curCooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        rotator = gun.GetComponent<Rotation>();
    }

    // Update is called once per frame
    void Update()
    {
        curCooldown = Mathf.Max(0f, curCooldown - Time.deltaTime);

        Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        rotator.FaceToward(mousePosition);

        if (Input.GetMouseButton(0))
        {
            if (curCooldown <= 0f)
			{
                curCooldown = cooldown;

                GameObject bulletObj = Instantiate(bulletPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                Bullet bullet = (Bullet)bulletObj.GetComponent<Bullet>();
                bullet.transform.position = gameObject.transform.position;
                bullet.setDirection(mousePosition - gameObject.transform.position);
                bullet.setSpeed(Globals.BULLET_SPEED);
            }
        }
    }
}
