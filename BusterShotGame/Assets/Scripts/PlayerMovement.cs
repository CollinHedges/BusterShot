using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // You can change this speed in the Inspector
    [SerializeField] private float movementSpeed = 5f;

    void Update()
    {
        // Only move if the left mouse button is held down
        if (Input.GetMouseButton(0))
        {
            // Convert the mouse screen position to a point in the 2D world
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f; // Ensure we keep our player on the same 2D plane

            // Move the player towards the mouse position
            transform.position = Vector3.MoveTowards(
                transform.position,
                mousePos,
                movementSpeed * Time.deltaTime
            );
        }
    }
}
