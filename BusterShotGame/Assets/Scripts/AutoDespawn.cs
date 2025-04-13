using UnityEngine;

public class AutoDespawn : MonoBehaviour
{
    [Header("Despawn Distance")]
    [SerializeField] private float despawnOffset = 2f; // How far off-screen before despawning

    void Update()
    {
        if (!IsInCameraView())
        {
            // If it's too far out, despawn
            Destroy(gameObject);
        }
    }

    private bool IsInCameraView()
    {
        Camera cam = Camera.main;
        float screenHalfHeight = cam.orthographicSize;
        float screenHalfWidth = screenHalfHeight * cam.aspect;

        // Enemy's current position
        Vector3 pos = transform.position;

        // Check if inside the camera bounds (plus some margin)
        if (pos.x < -(screenHalfWidth + despawnOffset) ||
            pos.x > (screenHalfWidth + despawnOffset) ||
            pos.y < -(screenHalfHeight + despawnOffset) ||
            pos.y > (screenHalfHeight + despawnOffset))
        {
            // Off-screen by your margin
            return false;
        }
        return true;
    }
}
