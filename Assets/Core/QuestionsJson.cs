using System.Collections.Generic;
//Container for the questions to be saved in a json file
[System.Serializable]
public class QuestionsJson
{
    public List<Question> savedQuestions;
    public QuestionsJson()
    {
        savedQuestions = new List<Question>();
    }
}
