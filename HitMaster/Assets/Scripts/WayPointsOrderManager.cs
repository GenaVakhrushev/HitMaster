using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WayPointsOrderManager : MonoBehaviour
{
    private float[] distancesToWayPoints;

    private static WayPoint[] wayPoints;
    public static WayPoint[] WayPoints => wayPoints;
    
    private void Start()
    {
        wayPoints = FindObjectsOfType<WayPoint>();

        distancesToWayPoints = new float[wayPoints.Length];

        CalculateDistancesToWayPoints();

        SortWayPointsByDistance();
    }
    
    private void CalculateDistancesToWayPoints()
    {
        NavMeshPath navMeshPath = new NavMeshPath();

        for (int i = 0; i < wayPoints.Length; i++)
        {
            PlayerMovement.Instance.NavMeshAgent.CalculatePath(wayPoints[i].transform.position, navMeshPath);

            if (navMeshPath.status == NavMeshPathStatus.PathComplete)
            {
                distancesToWayPoints[i] = GetPathLength(navMeshPath);
            }
            else
            {
                Debug.LogError("Agent can't reach point: " + wayPoints[i].name);
            }
        }
    }

    private float GetPathLength(NavMeshPath navMeshPath)
    {
        float len = 0;

        for (int i = 1; i < navMeshPath.corners.Length; i++)
        {
            len += Vector3.Distance(navMeshPath.corners[i - 1], navMeshPath.corners[i]);
        }

        return len;
    }

    //replace with move efficient algorithm if needed
    private void SortWayPointsByDistance()
    {
        for (int j = 0; j < wayPoints.Length - 1; j++)
        {
            for (int i = 0; i < wayPoints.Length - 1; i++)
            {
                if (distancesToWayPoints[i] > distancesToWayPoints[i + 1])
                {
                    float temp = distancesToWayPoints[i];
                    WayPoint tempWayPoint = wayPoints[i];

                    distancesToWayPoints[i] = distancesToWayPoints[i + 1];
                    distancesToWayPoints[i + 1] = temp;

                    wayPoints[i] = wayPoints[i + 1];
                    wayPoints[i + 1] = tempWayPoint;
                }
            }
        }
    }
}
