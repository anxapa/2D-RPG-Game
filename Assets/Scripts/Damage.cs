using UnityEngine;

public struct Damage
{
    public Vector3 origin;
    public int damageAmount;
    public float pushForce;

    public Damage(Vector3 origin, int damageAmount, float pushForce)
    {
        this.origin = origin;
        this.damageAmount = damageAmount;
        this.pushForce = pushForce;
    }
}
