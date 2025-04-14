using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // References
    public PlayerController player;
    // public weapon weapon...

    // Logic
    public int gold;
    public int experience;

    private void Awake()
    {
        // Assigns the GameManager as instance for easy referencing
        instance = this;


        // Once done loading a scene, these methods are called
        SceneManager.sceneLoaded += LoadState;
    }

    // Saves the game progress
    public void SaveState()
    {
        Debug.Log("SaveState");
    }

    // Loads the game progress
    public void LoadState(UnityEngine.SceneManagement.Scene scene, LoadSceneMode loadSceneMode)
    {
        SceneManager.sceneLoaded -= LoadState;
        Debug.Log("LoadState");
    }
}
