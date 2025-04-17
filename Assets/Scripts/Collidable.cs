using UnityEngine;

public class Collidable : MonoBehaviour
{
    [SerializeField] private ContactFilter2D _filter;
    private BoxCollider2D _boxCollider;
    private Collider2D[] _hits = new Collider2D[30];

    protected virtual void Start()
    {
        // Assign components
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        CheckForCollisions();
    }

    protected virtual void CheckForCollisions()
    {
        // Checks for collisions
        _boxCollider.Overlap(_filter, _hits);

        // Goes through the recent 10 collisions
        for (int i = 0; i < _hits.Length; i++)
        {
            // Activates the OnCollide function if it is a valid collision
            if (_hits[i] != null && !_hits[i].name.Equals("Collision"))
                OnCollide(_hits[i]);

            // The array is not cleaned up, so we do it ourself
            _hits[i] = null;
        }
    }

    // OnCollide behavior
    protected virtual void OnCollide(Collider2D coll)
    {
        Debug.Log($"OnCollide was not implemented in {this.name}");
    }
}
