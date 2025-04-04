using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    [SerializeField] private Transform lookAt;
    [SerializeField] private float boundX = 0.15f;
    [SerializeField] private float boundY = 0.05f;

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        // Checks if the player is outside of the bounds
        float deltaX = lookAt.position.x - transform.position.x;
        if (Mathf.Abs(deltaX) > boundX)
        {
            // Player is on the right of the bound.
            if (deltaX > 0)
            {
                delta.x = deltaX - boundX;
            }
            // Player is on the left of the left.
            else
            {
                delta.x = deltaX + boundX;
            }
        }

        float deltaY= lookAt.position.y - transform.position.y;
        if (Mathf.Abs(deltaY) > boundY)
        {
            // Player is on the top of the bound.
            if (deltaY > 0)
            {
                delta.y = deltaY - boundY;
            }
            // Player is on the bottom of the bound.
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        transform.position += delta;
    }
}
