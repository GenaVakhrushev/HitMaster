using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static UnityEvent OnGameStarted = new UnityEvent();

    public void StartGame()
    {
        OnGameStarted.Invoke();
    }
}
