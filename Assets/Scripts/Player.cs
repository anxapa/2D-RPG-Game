using Unity.VisualScripting;
using UnityEngine;

public class Player : Mover
{
    // Components
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private void FixedUpdate()
    {
        // Input handling
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(x, y, 0));
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        base.ReceiveDamage(dmg);
        GameManager.instance.OnHitpointChange();
    }
    public void SwapSprite(int spriteNumber)
    {
        _spriteRenderer.sprite = GameManager.instance.playerSprites[spriteNumber];
    }

    public void OnLevelUp()
    {
        maxHitpoints++;
        hitpoints = maxHitpoints;
    }

    public void SetLevel(int level)
    {
        // Players start at level 1, so level - 1 is to offset that
        for (int i = 0; i < level - 1; i++)
            OnLevelUp();
    }

    // Heals the player by a certain amount
    public void Heal(int healAmount)
    {
        if (hitpoints >= maxHitpoints)
            return;

        hitpoints += healAmount;

        if (hitpoints > maxHitpoints)
            hitpoints = maxHitpoints;
        
        GameManager.instance.ShowText($"+{healAmount} hp", 20, Color.green, transform.position, Vector3.up * 20f, 1f);
        GameManager.instance.OnHitpointChange();
    }
}
