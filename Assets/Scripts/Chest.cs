using UnityEngine;

public class Chest : Collectable
{
    [SerializeField] private Sprite _emptySprite;
    [SerializeField] private int _goldAmount;

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
            Debug.Log($"Granted {_goldAmount} gold!");
        }
    }
}
