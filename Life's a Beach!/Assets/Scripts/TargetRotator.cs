using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRotator : MonoBehaviour
{
    [SerializeField] float target;
    public float Target
    {
        get => target;
        set => target = value;
    }
    [SerializeField] bool useStartingRotation;
    [SerializeField] float force;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (useStartingRotation)
        {
            Target = transform.rotation.eulerAngles.z;
        }
    }

    private void FixedUpdate()
    {
        rb.MoveRotation
        (
            Mathf.LerpAngle(rb.rotation, target, force * Time.fixedDeltaTime)
        );
    }
}
