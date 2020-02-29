using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;
    Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bulletObj = Instantiate(bulletPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            Bullet bullet = (Bullet) bulletObj.GetComponent<Bullet>();
            bullet.transform.position = gameObject.transform.position;
            Vector2 mousePosition2d = camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mousePosition = new Vector3(mousePosition2d.x, mousePosition2d.y, 0);
            bullet.setDirection(mousePosition - gameObject.transform.position);
            bullet.setSpeed(Globals.BULLET_SPEED);
        }
    }
}
