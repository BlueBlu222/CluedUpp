using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
///Large singleton class for controlling the leaderboard
///Orders all scores when game object is enabled
///Saves them into and loads them from a json file
namespace Scoring
{
    public class Leaderboard : MonoBehaviour
    {
        public static Leaderboard singleton;
        static ScoresJson scores;
        static List<Highscore> shownScores;
        static Score score;
        static Highscore h;
        static string json;
        public GameObject scorePrefab;
        public int scoresDistance = -40;
        static TextMeshProUGUI winText;
        // Start is called before the first frame update
        void OnEnable()
        {
            if (singleton == null)
            {
                singleton = this;
                Setup();
                //  CreateScores();
                return;
            }
            else if (singleton != this)
            {
                Debug.LogError("Can't have more than one leaderboard");
                Destroy(this);
                return;
            }
            score = GameManager.CurrentScore;
            ShowWinText(score.score > 2);
            //   Debug.Log(scores.Count);
            CreateScores();
        }
        void ShowWinText(bool win)
        {
            Debug.Log(string.Format("You scored {0} in {1} seconds. You {2}", score.score, score.completionTime, win ? "win" : "lose"));
            winText.text = (win) ? "Win" : "Lose";
            winText.color = (win) ? Color.green : Color.red;
        }
        void CreateScores()
        {
            if (scores != null && scores.savedScores.Count >= 0)
            {
                scores.savedScores.Sort();
                //  scores.OrderBy(x => x.score).ToList();
                //Instantiates all missing highscores as well as the latest
                for (int j = shownScores.Count; j < scores.savedScores.Count; j++)
                {
                    //   Debug.Log(scores.savedScores[j].score + " " + scores.savedScores[j].completionTime);
                    h = GameObject.Instantiate(scorePrefab, transform).GetComponent<Highscore>();
                    h.Setup(scores.savedScores[j], j * scoresDistance, true);
                    shownScores.Add(h);
                }
                //Adds the latest score
                scores.savedScores.Add(score);
                scores.savedScores.Sort();
                h = GameObject.Instantiate(scorePrefab, transform).GetComponent<Highscore>();
                Debug.Log(scores.savedScores.IndexOf(score) - 1);
                h.Setup(score, (scores.savedScores.IndexOf(score) - 1) * scoresDistance);
                shownScores.Add(h);
                //Re-sorts the scores
                    for (int i = h.Place; i < scores.savedScores.Count; i++)
                    {

                        shownScores[i].Place++;
                    }
            }
        }
        static void Setup()
        {
            if (scores == null)
            {
                string json = PlayerPrefs.GetString("scores");
                //   Debug.Log(json);
                scores = json == "" ? new ScoresJson() : JsonUtility.FromJson<ScoresJson>(json);
            }
            if (shownScores == null)
            {
                shownScores = new List<Highscore>();
            }
            winText = GameObject.Find("WinText").GetComponent<TextMeshProUGUI>();
        }
        public static void Save()
        {
            if (h != null)
            {
                h.FinaliseNameText();
                score.name = h.nameText.text;
            }
            json = JsonUtility.ToJson(scores);
            //    Debug.Log(json);
            PlayerPrefs.SetString("scores", json);
            GameManager.leaderboard.SetActive(false);
        }
        public static int scoreCount
        {
            get
            {
                return scores.savedScores.Count();
            }
        }
    }
}
