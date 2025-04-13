using UnityEngine;

public class EnemyDrift : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float minSpeed = 0.5f;
    [SerializeField] private float maxSpeed = 2f;

    [Header("Angle Settings")]
    [Tooltip("How many degrees we can deviate from the exact line to center.")]
    [SerializeField] private float angleVariance = 30f;

    private float speed;
    private Vector2 driftDirection;

    void Start()
    {
        // Random speed between min/max
        speed = Random.Range(minSpeed, maxSpeed);

        // 1) Direct line from spawn to center
        Vector2 toCenter = Vector2.zero - (Vector2)transform.position;
        float baseAngle = Mathf.Atan2(toCenter.y, toCenter.x) * Mathf.Rad2Deg;

        // 2) Add random offset
        float randomOffset = Random.Range(-angleVariance, angleVariance);
        float finalAngle = baseAngle + randomOffset;

        // 3) Convert angle back to a direction
        float rad = finalAngle * Mathf.Deg2Rad;
        driftDirection = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
    }

    void Update()
    {
        // Move in driftDirection
        transform.Translate(driftDirection * speed * Time.deltaTime, Space.World);
    }
}
