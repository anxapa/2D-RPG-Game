using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    // Text fields
    [SerializeField] private TextMeshProUGUI _levelText, _hitpointText, _goldText,
        _upgradeCostText, _xpText;

    // Logic
    private int _currentCharacterSelection = 0;
    public Image _characterSelectionSprite;
    public Image _weaponSprite;
    public RectTransform _xpBar;

    // Character Selection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            _currentCharacterSelection++;

            // Makes sure we do not go outside of the sprite array bound
            if (_currentCharacterSelection >= GameManager.instance.playerSprites.Count)
                _currentCharacterSelection = 0;
        }
        else
        {
            _currentCharacterSelection--;

            // Makes sure we do not go outside of the sprite array bound
            if (_currentCharacterSelection < 0)
                _currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
        }

        OnSelectionChanged();
    }

    private void OnSelectionChanged()
    {
        _characterSelectionSprite.sprite = GameManager.instance.playerSprites[_currentCharacterSelection];
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
        // Weapon
        _weaponSprite.sprite = GameManager.instance.weaponSprites[0];
        _upgradeCostText.text = "NOT IMPLEMENTED";

        // Meta
        _levelText.text = "NOT IMPLEMENTED";
        _hitpointText.text = GameManager.instance.player.hitpoints.ToString();
        _goldText.text = GameManager.instance.gold.ToString();

        // XP Bar
        _xpText.text = "NOT IMPLEMENTED";
        _xpBar.localScale = new Vector3(0.5f, 1, 1);
    }
}
