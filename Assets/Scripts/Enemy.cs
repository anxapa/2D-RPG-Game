using UnityEngine;

public class Enemy : Mover
{
    [Header("Enemy Attributes")]
    // Experience
    public int xpValue = 1;

    // Logic
    public float triggerLength = 1;
    public float chaseLength = 5;
    private bool _chasing;
    private bool _collidingWithPlayer;
    private Transform _playerTransform;
    private Vector3 _startingPosition;

    // Hitbox
    private ContactFilter2D _filter;
    private BoxCollider2D _hitbox;
    private Collider2D[] _hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();

        _playerTransform = GameManager.instance.player.transform;
        _startingPosition = transform.position;
        _hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        // Is the player in range?
        // Handles movement
        CheckForPlayer();

        // Is the enemy currently colliding with the player?
        CheckForCollisions();
    }
    private void CheckForPlayer()
    {
        if (Vector3.Distance(_playerTransform.position, transform.position) < chaseLength)
        {
            if (Vector3.Distance(_playerTransform.position, transform.position) < triggerLength)
                _chasing = true;

            if (_chasing)
            {
                if (!_collidingWithPlayer)
                {
                    UpdateMotor((_playerTransform.position - transform.position).normalized);
                }
            }

            else
            {
                UpdateMotor(_startingPosition - transform.position);
            }
        }

        else
        {
            UpdateMotor(_startingPosition - transform.position);
            _chasing = false;
        }
    }
    private void CheckForCollisions()
    {
        // Checks for collisions
        _boxCollider.Overlap(_filter, _hits);

        _collidingWithPlayer = false;

        // Goes through the recent 10 collisions
        for (int i = 0; i < _hits.Length; i++)
        {
            // Activates the OnCollide function if it is a valid collision
            if (_hits[i] != null)
                if (_hits[i].CompareTag("Fighter") && _hits[i].name.Equals("Player"))
                    _collidingWithPlayer = true;

            // The array is not cleaned up, so we do it ourself
            _hits[i] = null;
        }
    }
    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantXP(xpValue);
        GameManager.instance.ShowText($"+ {xpValue} xp", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
    }
    protected override void ChangeSpriteToDirection()
    {
        if (_moveDelta.x < 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (_moveDelta.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
