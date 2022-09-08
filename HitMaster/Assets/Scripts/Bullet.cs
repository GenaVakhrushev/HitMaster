using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float hideDistance = 100;

    private Rigidbody rb;

    [SerializeField] private float damage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, Camera.main.transform.position) > hideDistance)
        {
            gameObject.SetActive(false);
        }
    }

    public void Launch(Vector3 dir, float speed)
    {
        rb.velocity = dir * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            return;
        }
        
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }

        gameObject.SetActive(false);
    }
}
