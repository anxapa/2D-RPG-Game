using UnityEngine;

public class Crate : Mover
{
    protected override void Death()
    {
        Destroy(gameObject);
    }
}
