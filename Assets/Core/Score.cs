using System;
/// <summary>
/// Serializable class to store scores with their completion time and username
/// </summary>
[Serializable]
public class Score : IComparable
{
    public string name;
    public int score, completionTime;
    public Score(string n, string s, int t)
    {
        name = n;
        score = int.Parse(s);
        completionTime = t;
    }
    public Score(string n, int i, int time)
    {
        name = n;
        score = i;
        completionTime = time;
    }
    public Score(int i, int time)
    {
        score = i;
        completionTime = time;
    }
    public int CompareTo(object other)
    {
        Score ther = (Score)other;
        if (ther.score == score)
        {
            return completionTime - ther.completionTime;
        } else
        {
            return ther.score - score;
        }
    }
}