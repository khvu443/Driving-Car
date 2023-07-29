using Assets.script;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance { get; private set; }

    [SerializeField] TMP_Text ScoreText;
    public static int Score = 0;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Score = SceneGameManager.score;
        ScoreText.text = "SCORE: " + Score;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void AddPoint()
    {
        Score += 1;
        ScoreText.text = "SCORE: " + Score;
    }

}
