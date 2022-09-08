using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    private float points;
    public float Points => points;

    public float MaxHP;

    public float HPPercent => Points / MaxHP;

    public UnityEvent OnChangeHP = new UnityEvent();
    public UnityEvent OnLostHP = new UnityEvent();

    private void Start()
    {
        points = MaxHP;
    }

    public void AddHP(float hp)
    {
        points += hp;

        OnChangeHP.Invoke();

        if(points > MaxHP)
        {
            points = MaxHP;
        }
    }

    public void RemoveHP(float hp)
    {
        points -= hp;

        OnChangeHP.Invoke();

        if (points <= 0)
        {
            OnLostHP.Invoke();
        }
    }
}
