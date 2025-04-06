using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    BoxCollider2D boxCollider;
    Vector3 moveDelta;
    RaycastHit2D hit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Input handling
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // Resetting moveDelta
        moveDelta = new Vector3(x, y, 0);

        // Changing sprite to direction
        if (x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Checks if player will hit anything on the x-direction using box casting
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), 
                Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider != null)
        {
            moveDelta.x = 0;
        }

        // Checks if player will hit anything on the y-direction using box casting
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), 
                Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider != null) {
            moveDelta.y = 0;
        }

        // Checks if player will hit anything on the xy-direction using box casting
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, moveDelta.y),
                Mathf.Abs(moveDelta.magnitude * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider != null)
        {
            moveDelta = Vector3.zero;
        }

        // Movement
        transform.Translate(moveDelta * Time.deltaTime);
    }
}
