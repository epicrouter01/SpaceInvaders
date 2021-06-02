using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreModel: Model
{
    public static string name = "Score";

    private int highscore = 0;

    public ScoreModel(): base(name)
    {
    }

    public int Highscore { get => highscore; set => highscore = value; }
}
