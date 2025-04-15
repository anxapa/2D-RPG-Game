using UnityEngine;

public class Chest : Collectable
{
    [SerializeField] private Sprite _emptySprite;
    [SerializeField] private int _goldAmount = 5;

    // Components
    private SpriteRenderer _spriteRenderer;

    protected override void Start()
    {
        base.Start();

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;

            _spriteRenderer.sprite = _emptySprite;
            GameManager.instance.gold += _goldAmount;
            GameManager.instance.ShowText($"+{_goldAmount} gold!", 25, Color.yellow, transform.position, Vector3.up * 25, 1f);
        }
    }
}
