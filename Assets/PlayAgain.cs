using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{

    Highscore highscore;
    // Start is called before the first frame update
    void Start()
    {
        highscore = GameObject.Find("Highscore").GetComponent<Highscore>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        highscore.highscore = 0;
        SceneManager.LoadScene("Main");
    }
}
