using UnityEngine;

public class Collectable : Collidable
{
    protected bool collected;

    protected override void OnCollide(Collider2D coll)
    {
        // Only collects if the player is colliding with it.
        if (coll.name.Equals("Player"))
            OnCollect();
    }

    protected virtual void OnCollect()
    {
        collected = true;
    }
}
