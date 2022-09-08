using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable
{
    private HP hp;
    private Ragdoll ragdoll;

    private Collider hitCollider;

    [SerializeField] private WayPoint wayPoint;

    private void Start()
    {
        hp = GetComponent<HP>();
        hp.OnLostHP.AddListener(Dead);

        ragdoll = GetComponent<Ragdoll>();
        ragdoll.Disable();

        hitCollider = GetComponent<Collider>();

        if (wayPoint)
        {
            wayPoint.AddEnemy(this);
        }
    }

    public void Dead()
    {
        ragdoll?.Enable();

        hitCollider.enabled = false;

        if (wayPoint)
        {
            wayPoint.RemoveEnemy(this);
        }
    }

    public void TakeDamage(float damage)
    {
        hp.RemoveHP(damage);
    }

    private void OnDestroy()
    {
        Dead();
    } 
}
