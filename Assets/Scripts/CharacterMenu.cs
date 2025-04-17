using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    // Text fields
    [SerializeField] private TextMeshProUGUI _levelText, _hitpointText, _goldText,
        _upgradeCostText, _xpText;

    // Logic
    public Image _characterSelectionSprite;
    public Image _weaponSprite;
    public RectTransform _xpBar;

    // Character Selection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            GameManager.instance.currentCharacterSelection++;

            // Makes sure we do not go outside of the sprite array bound
            if (GameManager.instance.currentCharacterSelection >= GameManager.instance.playerSprites.Count)
                GameManager.instance.currentCharacterSelection = 0;
        }
        else
        {
            GameManager.instance.currentCharacterSelection--;

            // Makes sure we do not go outside of the sprite array bound
            if (GameManager.instance.currentCharacterSelection < 0)
                GameManager.instance.currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
        }

        OnSelectionChanged();
    }

    private void OnSelectionChanged()
    {
        _characterSelectionSprite.sprite = GameManager.instance.playerSprites[GameManager.instance.currentCharacterSelection];
        GameManager.instance.player.SwapSprite(GameManager.instance.currentCharacterSelection);
    }

    // Weapon Upgrade
    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
        {
            UpdateMenu();
        }
    }

    // Update the character information
    public void UpdateMenu()
    {
        // Make sprite reload on open
        OnSelectionChanged();

        // Weapon
        _weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];

        // Weapon not maxed out
        if (GameManager.instance.weapon.weaponLevel < GameManager.instance.weaponPrices.Count)
            _upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        // Weapon maxed out
        else
            _upgradeCostText.text = "MAX";

        // Meta
        _levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        _hitpointText.text = $"{GameManager.instance.player.hitpoints} / {GameManager.instance.player.maxHitpoints}";
        _goldText.text = GameManager.instance.gold.ToString();

        // XP Bar
        int currentLevel = GameManager.instance.GetCurrentLevel();

        if (currentLevel >= GameManager.instance.xpTable.Count) // Checks if player is max level
        {
            _xpText.text = $"{GameManager.instance.experience} xp points";
            _xpBar.localScale = Vector3.one;
        }
        else
        {
            // Calculate current XP
            int prevXpNeeded = GameManager.instance.GetXpToLevel(currentLevel - 1);
            int currentXP = GameManager.instance.experience - prevXpNeeded;

            // Using current level - 1 since player level starts at 1 yet array starts at 0.
            _xpText.text = $"{currentXP} / {GameManager.instance.xpTable[currentLevel - 1]}";
            _xpBar.localScale = new Vector3((float) currentXP / GameManager.instance.xpTable[currentLevel - 1], 1f, 1f);
        }
    }
}
