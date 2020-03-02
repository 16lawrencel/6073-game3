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

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        rotator = gun.GetComponent<Rotation>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = GetWorldPositionOnPlane(Input.mousePosition, 0);

//        Debug.Log(mousePosition);
        rotator.FaceToward(mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject bulletObj = Instantiate(bulletPrefab, gameObject.transform.position + new Vector3(0, 0, 0.01f), Quaternion.identity);
            Bullet bullet = (Bullet) bulletObj.GetComponent<Bullet>();
            bullet.setDirection(mousePosition - gameObject.transform.position);
            bullet.setSpeed(Globals.BULLET_SPEED);
        }
    }
    
    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z) {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
