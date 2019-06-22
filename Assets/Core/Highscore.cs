using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Scoring
{
    public class Highscore : MonoBehaviour
    {
        Color[] placeColors = { Color.yellow, Color.grey, Color.red, Color.white };
        public static int addedPos = 30;
        Vector3 pos;
        public float speed = 1;
        public TextMeshProUGUI nameText, scoreText, placeText, timeText;
        TMP_InputField iField;
        int place;
        public int Place
        {
            get { return place; }
            set
            {
                pos = transform.parent.position + new Vector3(0, (value) * Leaderboard.singleton.scoresDistance, 0);
                place = value;
                placeText.text = value.ToString();
                //  Debug.Log(name + value);
                placeText.color = placeColors[(int)Mathf.Clamp(value, 1, 4) - 1];
            }
        }

        public void Setup(Score s, int p, bool finalised = false)
        {
            iField = GetComponentInChildren<TMP_InputField>();
            if (finalised)
            {
                FinaliseNameText(s.name);
            }
            else
            {
                iField.text = "AAA";
            }
            Place = ((p / Leaderboard.singleton.scoresDistance) + 1);
            name += Place;
            scoreText.text = s.score.ToString();
            timeText.text = s.completionTime.ToString() + "s";
        }
        public void FinaliseNameText(string start = "")
        {
            //  Debug.Log(start + iField.text);
            name = nameText.text = (!string.IsNullOrEmpty(start)) ? start : iField.text;
            iField.gameObject.SetActive(false);
        }
        public bool Finalised
        {
            get
            {
                return nameText.text == "";
            }
        }
        void Update()
        {
            Vector3 finalPos = pos + new Vector3(0, addedPos, 0);
            if (pos != null && transform.position != finalPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, finalPos, speed * Time.deltaTime);
            }
        }
    }
}
