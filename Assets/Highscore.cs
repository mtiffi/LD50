using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Highscore : MonoBehaviour
{

    public int highscore = 0;
    public string playerName;
    private TextMeshProUGUI text;
    private float pointTimer;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!text)
        {
            GameObject textHolder = GameObject.Find("HighscoreText");
            if (textHolder)
                text = textHolder.GetComponent<TextMeshProUGUI>();
        }
 
        if (text)
            text.text = highscore.ToString();
    }

    public void AddToHighscore(int score)
    {
        highscore += score;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
