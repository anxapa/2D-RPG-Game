using UnityEngine;

public class Portal : Collidable
{
    [SerializeField] private string[] _sceneNames;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name.Equals("Player"))
        {
            // Saves the state of the game
            GameManager.instance.SaveState();
            
            // Selects a random string within the scene list
            string sceneName = _sceneNames[Random.Range(0, _sceneNames.Length)];

            // Teleports the player to that scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
