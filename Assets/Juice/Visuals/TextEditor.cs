using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
