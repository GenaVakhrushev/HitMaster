using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private ObjectPool<Bullet> bullets;

    [SerializeField] private GameObject BulletPrefab;

    [SerializeField] private float shootingHeightOffset;

    [SerializeField, Range(1, 25)] private float bulletSpeed; 

    private void Start()
    {
        bullets = new ObjectPool<Bullet>(BulletPrefab, 10);
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector3 bulletSpawnPos = transform.position + transform.up * shootingHeightOffset;

        Bullet bullet = bullets.GetObject();

        bullet.transform.position = bulletSpawnPos;
        bullet.Launch(GetShotDir(bulletSpawnPos), bulletSpeed);
    }

    private Vector3 GetShotDir(Vector3 bulletSpawnPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return (hit.point - bulletSpawnPos).normalized;
        }

        Vector3 mouseMos = Input.GetTouch(0).position;
        mouseMos.z = Vector3.Distance(transform.position, Camera.main.transform.position) * 3;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseMos);
        return (mouseWorldPos - bulletSpawnPos).normalized; 
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position + transform.up * shootingHeightOffset, 0.1f);
    }
}
