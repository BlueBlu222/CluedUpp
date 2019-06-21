using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ScoresJson
{
    public List<Score> savedScores;
    public ScoresJson()
    {
        savedScores = new List<Score>();
    }
}
