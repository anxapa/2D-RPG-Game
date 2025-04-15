using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingText> _floatingTexts = new List<FloatingText>();

    // Show floating text
    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText floatingText = GetFloatingText();

        // Assigns parameters to floating text fields
        floatingText.txt.text = msg;
        floatingText.txt.fontSize = fontSize;
        floatingText.txt.color = color;

        // Transform position to screen space so we can use it
        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position);

        floatingText.motion = motion;
        floatingText.duration = duration;

        floatingText.Show();
    }

    private void Update()
    {
        foreach(FloatingText txt in _floatingTexts)
        {
            txt.UpdateFloatingText();
        }
    }

    // Object pool for floating text
    private FloatingText GetFloatingText()
    {
        // Combs through the floating texts array to find a non-active floating text.
        FloatingText txt = _floatingTexts.Find(t => !t.isActive);

        if (txt == null)
        {
            txt = new FloatingText();
            txt.go = Instantiate(textPrefab, textContainer.transform);
            txt.txt = txt.go.GetComponent<TextMeshProUGUI>();

            _floatingTexts.Add(txt);
        }

        return txt;
    }
}
