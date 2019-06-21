using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveRight : MonoBehaviour
{
    TextMeshProUGUI text;
    public float maxXPos = 1000, speed = 1;
    float startXPos;
    public void Move(bool correct)
    {
        if (text == null)
        {
            text = GetComponentInChildren<TextMeshProUGUI>();
            startXPos = GetComponent<RectTransform>().rect.position.x;
        }
        transform.position = new Vector3(startXPos, transform.position.y, 0);
        text.color = (correct) ? Color.green : Color.red;
        text.text = (correct) ? "Right!" : "Wrong!";
        if (GetComponent<Wiggle>())
        {
            Destroy(GetComponent<Wiggle>());
        }
    }
    void Update()
    {
        if (transform.localPosition.x < maxXPos)
        {
            transform.position += Vector3.right * speed;
        }
    }
}
