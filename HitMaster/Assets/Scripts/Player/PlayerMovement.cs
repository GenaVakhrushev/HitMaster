using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private int currentWayPointIndex = -1;

    private bool isRunning = true;

    //singleton
    private static PlayerMovement instance;
    public static PlayerMovement Instance => instance;

    private NavMeshAgent navMeshAgent;
    public NavMeshAgent NavMeshAgent => navMeshAgent;

    private WayPoint currentWayPoint => WayPointsOrderManager.WayPoints[currentWayPointIndex];

    private Animator animator;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        isRunning = navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance;

        animator.SetBool("IsRunning", isRunning);
    }

    private void LateUpdate()
    {
        if (!isRunning)
        {
            Quaternion lookRotation = Quaternion.LookRotation(currentWayPoint.transform.forward, transform.up);

            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, navMeshAgent.angularSpeed * Time.deltaTime);
        }
    }

    public void MoveToNextWayPoint()
    {
        currentWayPointIndex++;

        if(currentWayPoint.EnemiesCount <= 0 && currentWayPointIndex != WayPointsOrderManager.WayPoints.Length - 1)
        {
            MoveToNextWayPoint();
            return;
        }

        currentWayPoint.OnAllEnemiesKilled.AddListener(MoveToNextWayPoint);
        
        navMeshAgent.SetDestination(currentWayPoint.transform.position);
    }
}
