using UnityEngine;

public class EnemyHitbox : Collidable
{
    // Damage
    public int damage = 1;
    public float pushForce;

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.CompareTag("Fighter") && coll.name.Equals("Player"))
        {
            // Create new Damage object, and send it to fighter.
            Damage dmg = new Damage(transform.position, damage, pushForce);

            coll.SendMessage("ReceiveDamage", dmg);
        }
    }
}
