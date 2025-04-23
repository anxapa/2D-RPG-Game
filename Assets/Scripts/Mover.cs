using UnityEngine;

public abstract class Mover : Fighter
{
    [Header("Mover Attributes")]
    protected BoxCollider2D _boxCollider;
    protected Vector3 _moveDelta;
    protected RaycastHit2D _hit;

    // Speed of movement
    [SerializeField] protected float _speedMultiplier = 1f;
    protected float _ySpeed = 0.75f;
    protected float _xSpeed = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        _moveDelta = new Vector3(input.x * _xSpeed, input.y * _ySpeed, 0) * _speedMultiplier;

        // Set movement to push direction if there is any
        if (pushDirection != Vector3.zero)
        {
            _moveDelta = pushDirection;
        }
        // Prevent sprite from changing by getting hit
        else
        {
            // Changing sprite to direction
            ChangeSpriteToDirection();
        }

        // Reduce push force every frame, based off recovery speed
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);
        

        // Checks if player will hit anything on the x-direction using box casting
        _hit = Physics2D.BoxCast(transform.position, _boxCollider.size, 0, new Vector2(_moveDelta.x, 0),
                Mathf.Abs(_moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (_hit.collider != null)
        {
            _moveDelta.x = 0;
        }

        // Checks if player will hit anything on the y-direction using box casting
        _hit = Physics2D.BoxCast(transform.position, _boxCollider.size, 0, new Vector2(0, _moveDelta.y),
                Mathf.Abs(_moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (_hit.collider != null)
        {
            _moveDelta.y = 0;
        }

        // Checks if player will hit anything on the xy-direction using box casting
        _hit = Physics2D.BoxCast(transform.position, _boxCollider.size, 0, new Vector2(_moveDelta.x, _moveDelta.y),
                Mathf.Abs(_moveDelta.magnitude * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (_hit.collider != null)
        {
            _moveDelta = Vector3.zero;
        }

        // Movement
        transform.Translate(_moveDelta * Time.deltaTime);
    }

    protected virtual void ChangeSpriteToDirection()
    {
        if (_moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (_moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
