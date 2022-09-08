using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WayPoint : MonoBehaviour
{
    private List<Enemy> enemies;

    public int EnemiesCount => enemies.Count;

    public UnityEvent OnAllEnemiesKilled = new UnityEvent();

    private void Awake()
    {
        enemies = new List<Enemy>();
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);

        if(EnemiesCount <= 0)
        {
            OnAllEnemiesKilled.Invoke();

            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, 0.25f);
    }
}
