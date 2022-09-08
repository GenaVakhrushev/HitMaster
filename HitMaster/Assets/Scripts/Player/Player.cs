using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Shooting shooting;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        shooting = GetComponent<Shooting>();

        Disable();

        GameManager.OnGameStarted.AddListener(Enable);
        GameManager.OnGameStarted.AddListener(playerMovement.MoveToNextWayPoint);
    }

    public void Enable()
    {
        playerMovement.enabled = true;
        shooting.enabled = true;
    }

    public void Disable()
    {
        playerMovement.enabled = false;
        shooting.enabled = false;
    }
}
