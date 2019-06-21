/// <summary>
/// Question class containing strings for the question, it's answer and the three false answers
/// </summary>
[System.Serializable]
public class Question
{
    public string question, answer;
    public string[] falseAnswers = new string[3];
    public Question(string q, string a)
    {
        question = q;
        answer = a;
    }
}