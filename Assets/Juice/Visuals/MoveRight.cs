using UnityEngine;
using UnityEngine.UI;
using TMPro;
///Continuously moves an object to the right
///Used for visual feedback after answering a question

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
