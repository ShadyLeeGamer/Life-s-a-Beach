using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyboardController : MonoBehaviour, ISubmergeable
{
    [Header("Movement")]
    [SerializeField] float maxSpeedAirborne;
    [SerializeField] float maxSpeedUnderwater;
    float moveSpeed;
    [SerializeField] float moveSpeedSmoothTime;
    float moveSpeedSmoothVelocity;
    Vector2 directionalInput;
    public bool IsUnderwater { get; set; }

    [Header("Rotation")]
    [SerializeField] float maxRotateSpeed;
    float rotateSpeed;
    [SerializeField] float rotateSpeedSmoothTime;
    float rotateSpeedSmoothVelocity;

    TargetRotator targetRotator;

    Rigidbody2D rb;

    public Collider2D Collider { get; private set; }

    private void Awake()
    {
        Collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        targetRotator = GetComponent<TargetRotator>();
    }

    private void Update()
    {
        directionalInput = new Vector2
        (
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        float targetMoveSpeed = directionalInput.y == 0 ? 0f
            : (IsUnderwater ? maxSpeedUnderwater : maxSpeedAirborne) * Mathf.Sign(directionalInput.y);
        moveSpeed = Mathf.SmoothDamp(moveSpeed, targetMoveSpeed, ref moveSpeedSmoothVelocity, moveSpeedSmoothTime);

        float targetRotateSpeed = directionalInput.x == 0 ? 0f : maxRotateSpeed * Mathf.Sign(-directionalInput.x);
        rotateSpeed = Mathf.SmoothDamp(rotateSpeed, targetRotateSpeed, ref rotateSpeedSmoothVelocity, rotateSpeedSmoothTime);
        targetRotator.Target += rotateSpeed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        rb.velocity = transform.up * moveSpeed * Time.fixedDeltaTime;
    }
}
