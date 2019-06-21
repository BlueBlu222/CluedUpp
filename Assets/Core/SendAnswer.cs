using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SendAnswer : MonoBehaviour
{
    MoveRight mr;
    AudioSource aso;
    public AudioClip correct, incorrect;
    public TextMeshProUGUI questionText, timeText;
    public TextMeshProUGUI[] buttons;
    public List<Question> questions;
    List<Question> loadedQuestions;
    [Range(0,5)]
    public int leaderboardScrollSpeed = 2;
    int questionNumber = -1, currentQuestion = 0;
    public static int score = 0;
    string correctAnswer;
    public GameObject leaderboard;
    static string json;
    QuestionsJson qj;
    void Start()
    {
        mr = FindObjectOfType<MoveRight>();
        aso = GetComponent<AudioSource>();
        if(aso == null)
        {
            aso = gameObject.AddComponent<AudioSource>();
        }
        if (leaderboard == null)
        {
            leaderboard = GameObject.Find("Leaderboard");
        }
        //Initialises question text
        if (questionText == null)
        {
            Debug.LogError("set up question text in the game manager");
            return;
        }
        questionText.text = "";
        //Initialises time text
        if (timeText == null)
        {
            Debug.LogWarning("set up timer text in the game manager");
            GameObject g = GameObject.Find("Timer");
            if (g != null)
            {
                timeText = g.GetComponent<TextMeshProUGUI>();
                timeText.text = "";
            }
        }
        Restart();
        //Automatically assigns questions to the json file provided
        if (string.IsNullOrEmpty(json))
        {
            json = PlayerPrefs.GetString("questions");
        }
        qj = JsonUtility.FromJson<QuestionsJson>(json);
        if (qj == null || qj.savedQuestions == null)
        {
            SaveJson();
        } else
        {
            Debug.Log(json);
            questions = qj.savedQuestions;
        }
        //Returns error if no questions are available
        if (questions == null || questions.Count == 0)
        {
            questions = new List<Question>();
            Debug.LogError("set up questions in the game manager");
        } else if (questions.Count < 10)
        {
            Debug.LogError("set up more questions in the game manager");
        }
        //Ensures buttons have been set up
        if (buttons.Length != 4)
        {
            Debug.LogError("set up buttons in the game manager " + buttons.Length);
           /* Button[] tempB = FindObjectsOfType<Button>();
            if (tempB.Length == 4)
            {
                buttons = new TextMeshProUGUI[4];
                for (int i = 0; i < 4; i++)
                {
                    buttons[i] = tempB[i].GetComponentInChildren<TextMeshProUGUI>();
                }
            }
            else
            {
                Debug.LogError("failed to automaticlly set up buttons " + buttons.Length);
            }*/
        }
    }
    void SaveJson()
    {
        json = JsonUtility.ToJson(qj);
        PlayerPrefs.SetString("questions", json);
    }
    public void Restart()
    {
        loadedQuestions = new List<Question>();
        loadedQuestions.AddRange(questions);
        Leaderboard.Save();
        questionNumber = -1;
        score = 0;
        completionTime = 0;
        LoadNextQuestion();
    }
    //Not gonna work
    public void Submit(string myAnswer)
    {
     //   Debug.Log(correctAnswer + myAnswer);
        if (correctAnswer == myAnswer)
        {
            Debug.Log("correct");
            aso.PlayOneShot(correct);
            score++;
            mr.Move(true);
        }
        else
        {
            Debug.Log("incorrect");
            aso.PlayOneShot(incorrect);
            mr.Move(false);
        }
        questionText.text = "";
        loadedQuestions.RemoveAt(currentQuestion);
        StartCoroutine(VisualFeedback());
    }
    IEnumerator VisualFeedback()
    {
        Wiggle.canSpin = true;
        yield return new WaitForSeconds(5);
        Wiggle.canSpin = false;
        LoadNextQuestion();
    }
    public void LoadNextQuestion()
    {
        questionNumber++;
        if (loadedQuestions.Count < 1)
        {
            Debug.Log("end of questions. " + ((score >2) ? "You win" : "You lose"));
            leaderboard.SetActive(true);
        }
        else
        {
            //   Debug.Log(correctAnswer);

            //You can choose to get the questions in order or a random order
            //   questionText.text = GetNextQuestion;
            questionText.text = GetRandomQuestion;
            AssignButtons();
        }
    }
    //Assigns a random answer to each button, including the text that was already there.
    void AssignButtons()
    {
      //  Debug.Log(currentQuestion + loadedQuestions[currentQuestion].question + loadedQuestions[currentQuestion].answer);
        List<TextMeshProUGUI> tempList = new List<TextMeshProUGUI>();
        tempList.AddRange(buttons);
        for(int i = 0; i < 3; i++)
        {
            int j = Random.Range(0, tempList.Count);            
            tempList[j].text = string.Format("{0}) {1}", tempList[j].text[0], Punctualise(loadedQuestions[currentQuestion].falseAnswers[i]));
            tempList.RemoveAt(j);
        }
        //Sets up the correct answer
        tempList[0].text = string.Format("{0}) {1}", tempList[0].text[0], Punctualise(loadedQuestions[currentQuestion].answer));
        correctAnswer = "" + tempList[0].text[0];
    }
    void Update()
    {
        if (leaderboard.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Restart();
            }
            Highscore.addedPos += leaderboardScrollSpeed * (int)Input.GetAxis("Vertical");
            Highscore.addedPos = Mathf.Clamp(Highscore.addedPos,0,Leaderboard.scoresDistance * -1 * (Leaderboard.scoreCount - 1));
        } else
        {
            completionTime += Time.deltaTime;
            if (timeText != null)
            {
                timeText.text = "" + (int)completionTime;
            }
        }
    }
    static float completionTime;
    public static Score CurrentScore
    {
        get
        {
            return new Score(score,(int)completionTime);
        }
    }
    #region Question Getters
    string GetNextQuestion
    {
        get
        {
            currentQuestion = questionNumber;
            return FormatQuestion(loadedQuestions[questionNumber]);
        }
    }
    string GetRandomQuestion
    {
        get
        {
            currentQuestion = Random.Range(0, loadedQuestions.Count);
            return FormatQuestion(loadedQuestions[currentQuestion]);
        }
    }
    #endregion
    #region Formatting
    //Formats the stored question
    string FormatQuestion(Question q)
    {
            string s = q.question;
            //Adds a question mark if not already present 
            if (s[s.Length - 1] != '?')
            {
                s += "?";
            }
            s = string.Format("Question {0}: {1}", questionNumber + 1, s);
            return s;

    }
    //Ensures punctuation is correct. Adds a capital letter if not already present
    string Punctualise(string s)
    {
            if (!char.IsUpper(s[0]))
            {
                s = string.Format("{0}{1}", char.ToUpper(s[0]), s.Substring(1, s.Length - 1));
        }
        //Should add a capital letter after any punctuation
        return s;
    }
    #endregion
}
