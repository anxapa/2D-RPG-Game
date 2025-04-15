using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public bool isActive;
    public GameObject go;
    public TextMeshProUGUI txt;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    // Shows the floating text
    public void Show()
    {
        isActive = true;
        lastShown = Time.time;
        go.SetActive(isActive);
    }


    // Hides the floating text
    public void Hide()
    {
        isActive = false;
        go.SetActive(isActive);
    }

    // Updates the floating text
    public void UpdateFloatingText()
    {
        if (!isActive)
            return;

        // Checks if active time is longer than duration, if so then hide
        if (Time.time - lastShown > duration)
            Hide();

        // Motion movement
        go.transform.position += motion * Time.deltaTime;
    }

}
