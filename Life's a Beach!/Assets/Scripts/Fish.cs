using Bundos.WaterSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : Enemy
{
    [SerializeField] float moveSpeed;

    [SerializeField] Water water;
    Vector2 destination;
    [SerializeField] float destinationReachMinDistance;

    [SerializeField] float moveDirSmoothTime;
    Vector2 moveDir;
    Vector2 moveDirSmoothVelocity;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init()
    {
        SetNewDestination();
    }

    private void Start()
    {
        SetNewDestination();
    }

    private void Update()
    {
        Vector2 targetMoveDir = (destination - (Vector2)transform.position).normalized;
        moveDir = Vector2.SmoothDamp(moveDir, targetMoveDir, ref moveDirSmoothVelocity, moveDirSmoothTime);
        Debug.DrawRay(transform.position, moveDir);
    }

    private void FixedUpdate()
    {
        rb.velocity = moveSpeed * moveDir * Time.fixedDeltaTime;

        if (Vector2.Distance(transform.position, destination) <= destinationReachMinDistance)
        {
            SetNewDestination();
        }
    }

    void SetNewDestination()
    {
        destination = water.GetRandomPoint();
    }
}
