using UnityEngine;

public class IntroNPC : Collidable
{
    public string message;

    private float _cooldown = 4.0f;
    private float _lastShout;

    protected override void Start()
    {
        base.Start();
        _lastShout = -_cooldown;
    }
    protected override void OnCollide(Collider2D coll)
    {
        // Checks if its the cooldown is up
        if (Time.time - _lastShout > _cooldown)
        {
            _lastShout = Time.time;
            GameManager.instance.ShowText(message, 25, Color.white, transform.position + Vector3.up * 0.16f, Vector3.zero, _cooldown);
        }
    }
}
