using UnityEngine;

public class WaterBlaster : MonoBehaviour, ISubmergeable
{
    [SerializeField] TargetRotator armRotator;
    [SerializeField] Transform firePoint;
    [SerializeField] float range;
    Vector2 aimDir;

    [SerializeField] WaterBlast blastPrefab;

    SpriteRenderer spriteRenderer;

    [SerializeField] bool flip;

    [SerializeField] float shootInterval;
    bool CanShoot => shootCooldown <= 0f && !IsUnderwater;

    public bool IsUnderwater { get; set; }

    float shootCooldown;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!CanShoot)
        {
            shootCooldown -= Time.deltaTime;
        }
    }

    public void ProcessInputs(Vector2 aimInput, bool shootInput)
    {
        UpdateAim(aimInput);
        if (shootInput && CanShoot)
        {
            Shoot();
        }
    }

    void UpdateAim(Vector2 target)
    {
        aimDir = (target - (Vector2)armRotator.transform.position).normalized;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        armRotator.Target = angle;

        bool flipSprite = angle > 90 || angle < -90;
        if (flip)
        {
            flipSprite = !flipSprite;
        }
        spriteRenderer.flipY = flipSprite;
    }

    void Shoot()
    {
        Vector2 endPos = (Vector2)firePoint.position + aimDir * range;
        WaterBlast waterBlast = Instantiate(blastPrefab, firePoint.position, Quaternion.identity);
        waterBlast.Init(endPos);
        shootCooldown = shootInterval;
    }
}
