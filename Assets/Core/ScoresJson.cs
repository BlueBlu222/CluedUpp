using System.Collections.Generic;
//Container for the scores to be saved in a json file
[System.Serializable]
public class ScoresJson
{
    public List<Score> savedScores;
    public ScoresJson()
    {
        savedScores = new List<Score>();
    }
}
