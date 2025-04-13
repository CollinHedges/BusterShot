using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PulseVisual : MonoBehaviour
{
    [Header("Scale (Visual)")]
    [SerializeField] private float minScale = 0.1f;    // Start sprite size
    [SerializeField] private float maxScale = 3f;      // End sprite size
    [SerializeField] private float expandDuration = 0.5f;

    [Header("Collider (Actual Hit Radius)")]
    [SerializeField] private float minColliderRadius = 0.05f; // Start collision radius
    [SerializeField] private float maxColliderRadius = 1.5f;  // End collision radius

    [Header("Damage & Fade")]
    [SerializeField] private bool fadeOut = true;
    [SerializeField] private bool destroyAfterExpansion = true;
    private float damage;  // set via SetDamage() from PlayerAttack

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private bool rotateClockwise = true;

    [Header("Gizmo Settings")]
    [SerializeField] private bool showColliderGizmo = true; // Toggle to see wire circle in-game

    private float timer = 0f;
    private SpriteRenderer sr;
    private CircleCollider2D circleCol;

    void Awake()
    {
        // Grab references
        sr = GetComponent<SpriteRenderer>();
        circleCol = GetComponent<CircleCollider2D>();

        // Ensure the collider is a trigger
        circleCol.isTrigger = true;

        // Ensure rigidbody is Kinematic (so trigger collisions work properly)
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0;

        // Initialize visual scale & collider radius
        transform.localScale = Vector3.one * minScale;
        circleCol.radius = minColliderRadius;
    }

    void Update()
    {
        // Expand from min -> max over expandDuration
        timer += Time.deltaTime;
        float fraction = Mathf.Clamp01(timer / expandDuration);

        // 1) Grow the visual sprite
        float currentScale = Mathf.Lerp(minScale, maxScale, fraction);
        transform.localScale = Vector3.one * currentScale;

        // 2) Grow the collider radius
        circleCol.radius = Mathf.Lerp(minColliderRadius, maxColliderRadius, fraction);

        // 3) Rotate
        float spinDir = rotateClockwise ? -1f : 1f;
        transform.Rotate(0f, 0f, rotationSpeed * spinDir * Time.deltaTime);

        // 4) Fade out
        if (fadeOut && sr != null)
        {
            Color c = sr.color;
            c.a = 1f - fraction;
            sr.color = c;
        }

        // 5) Destroy at end
        if (destroyAfterExpansion && fraction >= 1f)
        {
            Destroy(gameObject);
        }
    }

    // Called by PlayerAttack script to pass damage
    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    // Trigger damage on first contact
    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy e = other.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(damage);
        }
    }

    // Gizmos (draw a wire circle showing the collider range)
    void OnDrawGizmos()
    {
        // Only draw if toggled on
        if (!showColliderGizmo) return;

        // If we're in play mode, use the runtime radius. Otherwise, approximate.
        if (Application.isPlaying && circleCol != null)
        {
            // The actual collision radius in world space
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, circleCol.radius);
        }
        else
        {
            // Preview in edit mode
            float previewRadius = minColliderRadius;
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, previewRadius);
        }
    }
}
