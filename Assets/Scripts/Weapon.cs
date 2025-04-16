using UnityEngine;

public class Weapon : Collidable
{
    // Damage struct
    public int damagePoint = 1;
    public float pushForce = 2.0f;

    // Upgrade
    public int weaponLevel = 0;
    private SpriteRenderer _spriteRenderer;

    // Swing
    [SerializeField] private float _cooldown = 0.5f;
    private Animator _animator;
    private float _lastSwing; 

    protected override void Start()
    {
        base.Start();

        // Get components
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - _lastSwing > _cooldown)
            {
                _lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.CompareTag("Fighter") && !coll.name.Equals("Player"))
        {
            Debug.Log(coll.name);

            // Create new Damage object, and send it to fighter.
            Damage dmg = new Damage(transform.position, damagePoint, pushForce);

            coll.SendMessage("ReceiveDamage", dmg);
        }
    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        _spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

        // Change stats
    }
    private void Swing()
    {
        _animator.SetTrigger("Swing");
    }
}
