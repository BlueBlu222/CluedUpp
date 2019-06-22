using UnityEngine;
using UnityEngine.UI;
using TMPro;
//Customises all texts in the scene at start
public class TextEditor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI[] texts = FindObjectsOfType<TextMeshProUGUI>();
        foreach (TextMeshProUGUI t in texts)
        {
            Wiggle w = t.gameObject.AddComponent<Wiggle>();
        }
    }
}
