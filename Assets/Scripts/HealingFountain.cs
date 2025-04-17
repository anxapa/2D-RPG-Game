using UnityEngine;

public class HealingFountain : Collidable
{
    [SerializeField] private int _healingAmount = 1;
    [SerializeField] private float _healCooldown = 1.0f;
    private float lastHeal;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag("Fighter") && coll.name.Equals("Player"))
        {
            if (Time.time - lastHeal > _healCooldown)
            {
                lastHeal = Time.time;
                GameManager.instance.player.Heal(_healingAmount);
            }
        }
    }
}
