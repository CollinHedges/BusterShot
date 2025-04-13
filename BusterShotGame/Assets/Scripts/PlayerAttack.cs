using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Pulse Settings")]
    [SerializeField] private float pulseDamage = 10f;
    [SerializeField] private float pulseCooldown = 2f;

    [Header("Visual Pulse Prefab")]
    [SerializeField] private GameObject pulseEffectPrefab;

    private float nextPulseTime = 0f;

    void Update()
    {
        if (Time.time >= nextPulseTime)
        {
            // Spawn the pulse ring
            PulseAttack();
            nextPulseTime = Time.time + pulseCooldown;
        }
    }

    private void PulseAttack()
    {
        if (pulseEffectPrefab != null)
        {
            // Instantiate the ring at the player's position
            GameObject ring = Instantiate(pulseEffectPrefab, transform.position, Quaternion.identity);

            // Pass damage to the ring's script
            PulseVisual ringScript = ring.GetComponent<PulseVisual>();
            if (ringScript != null)
            {
                ringScript.SetDamage(pulseDamage);
            }
        }
    }
}
