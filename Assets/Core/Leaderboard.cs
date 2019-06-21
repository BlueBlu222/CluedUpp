using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Leaderboard : MonoBehaviour
{
    static Leaderboard singleton;
    static ScoresJson scores;
    static List<Highscore> shownScores;
    static Score score;
    static Highscore h;
    static string json;
    public GameObject scorePrefab;
    public static int scoresDistance = -30;
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
        score = SendAnswer.CurrentScore;
        Debug.Log(string.Format("You scored {0} in {1} seconds", score.score, score.completionTime));
        //   Debug.Log(scores.Count);
        CreateScores();
    }
    void CreateScores()
    {
        if (scores != null && scores.savedScores.Count - 1 >= 0)
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
            h.Setup(score, (scores.savedScores.IndexOf(score) - 1) * scoresDistance);
            shownScores.Add(h);
            //Re-sorts the scores
            for (int i = scores.savedScores.IndexOf(score); i < scores.savedScores.Count; i++)
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
            Debug.Log(json);
            //    score = json == "" ? null : JsonUtility.FromJson<Score>(json);
            //    scores = new List<Score>();
            scores = json == "" ? new ScoresJson() : JsonUtility.FromJson<ScoresJson>(json);
        }
        if (shownScores == null)
        {
            shownScores = new List<Highscore>();
        }
    }
    public static void Save()
    {/*
        while (shownScores.Count > 0)
        {
            if (shownScores[0] != null)
            {
                Destroy(shownScores[0].gameObject);
            }
            else
            {
                shownScores.RemoveAt(0);
            }
        }*/
        if (h != null)
        {
            h.FinaliseNameText();
            score.name = h.nameText.text;
        }
        json = JsonUtility.ToJson(scores);
        //    Debug.Log(json);
        PlayerPrefs.SetString("scores", json);
        singleton.transform.parent.gameObject.SetActive(false);
    }
    public static int scoreCount
    {
        get
        {
            return scores.savedScores.Count();
        }
    }
}
