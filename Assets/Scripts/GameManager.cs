using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // Logic
    [Header("Logic")]
    public int gold;
    public int experience;

    private void Awake()
    {
        // Makes sure there is only one GameManager in the game
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Assigns the GameManager as instance for easy referencing
        instance = this;

        // Once done loading a scene, these methods are called
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
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

        if (gold >= weaponPrices[weapon.weaponLevel])
        {
            gold -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
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

        s += "0" + "|"; // preferredSkin
        s += gold.ToString() + "|"; // gold
        s += experience.ToString() + "|";   // experience
        s += "0";   // weaponLevel

        PlayerPrefs.SetString("SaveState", s);
    }

    // Loads the game progress
    public void LoadState(UnityEngine.SceneManagement.Scene scene, LoadSceneMode loadSceneMode)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        // preferredSkin = int.Parse(data[0]);
        gold = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        // weaponLevel = int.Parse(data[3]);

        Debug.Log("LoadState");
    }
}
