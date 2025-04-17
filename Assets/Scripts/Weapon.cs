using UnityEngine;

public class Weapon : Collidable
{
    // Damage struct
    public int[] damagePoint = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
    public float[] pushForce = {4.0f, 4.5f, 5.0f, 5.5f, 6.0f, 6.5f, 7.0f, 7.5f, 8.0f, 8.5f};

    // Upgrade
    public int weaponLevel = 0;
    public SpriteRenderer _spriteRenderer;

    // Swing
    [SerializeField] private float _cooldown = 0.5f;
    private Animator _animator;
    private float _lastSwing; 

    protected override void Start()
    {
        base.Start();

        // Get components
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
            Damage dmg = new Damage(transform.position, damagePoint[weaponLevel], pushForce[weaponLevel]);

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

    // Sets the weapon level to an integer and assigns the sprite accordingly
    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        _spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
}
