using System.Collections.Generic;
[System.Serializable]
public class QuestionsJson
{
    public List<Question> savedQuestions;
    public QuestionsJson()
    {
        savedQuestions = new List<Question>();
    }
}
