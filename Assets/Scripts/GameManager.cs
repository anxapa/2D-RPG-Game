using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Resources
    [Header("Resources")]
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // References
    [Header("References")]
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointBar;
    public GameObject menu;
    public GameObject hud;

    // Logic
    [Header("Logic")]
    public int gold;
    public int experience;
    public int currentCharacterSelection = 0;

    private void Awake()
    {
        // Makes sure there is only one GameManager and other components in the game
        if (instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(menu);
            Destroy(hud);
            return;
        }

        // Assigns the GameManager as instance for easy referencing
        instance = this;

        // Once done loading a scene, these methods are called
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Show floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    public bool TryUpgradeWeapon()
    {
        // Is the weapon max level?
        if (weaponPrices.Count <= weapon.weaponLevel)
            return false;

        // Can the player buy the weapon?
        if (gold >= weaponPrices[weapon.weaponLevel])
        {
            gold -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    // Experience System
    public int GetCurrentLevel()
    {
        int level = 0;
        int expNeeded = 0;

        // Recursively levels player up and adds the needed exp to the variable for checking.
        while (experience > expNeeded && level < xpTable.Count)
        {
            expNeeded += xpTable[level];
            level++;
        }

        return level;
    }

    public int GetXpToLevel(int level)
    {
        int xpNeeded = 0;

        for (int i = 0; i < level; i++)
            xpNeeded += xpTable[i];

        return xpNeeded;
    }

    public void GrantXP(int xp)
    {
        int currentLevel = GetCurrentLevel();
        experience += xp;
        if (currentLevel < GetCurrentLevel())
            OnLevelUp();
    }

    private void OnLevelUp()
    {
        ShowText("Level up!", 30, Color.blue, player.transform.position, Vector3.up * 40f, 2f);
        player.OnLevelUp();
        OnHitpointChange();
    } 

    public void OnHitpointChange()
    {
        float ratio = (float)player.hitpoints / (float)player.maxHitpoints;
        hitpointBar.localScale = new Vector3(1f, ratio, 1f);
    }

    public void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode loadSceneMode)
    {
        // Set player position to scene's spawn point
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    // Saves the game progress
    /*
     * INT preferredSkin
     * INT gold
     * INT experience
     * INT weaponLevel
     */
    public void SaveState()
    {
        // The data being stored for the GameState
        string s = "";

        s += currentCharacterSelection.ToString() + "|";
        s += gold.ToString() + "|"; // gold
        s += experience.ToString() + "|";   // experience
        s += weapon.weaponLevel.ToString(); // weaponLevel

        PlayerPrefs.SetString("SaveState", s);
    }

    // Loads the game progress
    public void LoadState(UnityEngine.SceneManagement.Scene scene, LoadSceneMode loadSceneMode)
    {
        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        // Get player sprite
        currentCharacterSelection = int.Parse(data[0]);
        player.SwapSprite(currentCharacterSelection);

        // Get gold
        gold = int.Parse(data[1]);
        
        // Get XP 
        experience = int.Parse(data[2]);
        player.SetLevel(GetCurrentLevel());

        // Change weapon level
        weapon.SetWeaponLevel(int.Parse(data[3]));
    }
}
