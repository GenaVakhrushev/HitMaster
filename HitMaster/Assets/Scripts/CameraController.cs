using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Vector3 posOffset;

    [SerializeField] private Vector3 lookAdditionalAngles;

    [SerializeField, Range(1, 10)] private float moveSpeed;
    [SerializeField, Range(15, 360)] private float rotationSpeed;

    private void LateUpdate()
    {
        Vector3 toTarget = target.position - transform.position;
        Quaternion toTargetRotation = Quaternion.LookRotation(toTarget);
        Quaternion correctedToTargetRotation = toTargetRotation * Quaternion.Euler(lookAdditionalAngles);

        transform.Translate((toTarget + target.TransformVector(posOffset)) * moveSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Lerp(transform.rotation, correctedToTargetRotation, rotationSpeed * Time.deltaTime);
    }
}
