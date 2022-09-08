using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    private Rigidbody[] rigidbodies;

    private Animator animator;

    void Awake()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();

        animator = GetComponentInChildren<Animator>();
    }

    public void Enable()
    {
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = false;
        }

        animator.enabled = false;
    }

    public void Disable()
    {
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = true;
        }

        animator.enabled = true;
    }
}
