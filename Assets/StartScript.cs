using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartScript : MonoBehaviour
{
    public TextMeshProUGUI please, playerName;
    public Highscore highscore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClick()
    {
        Debug.Log(playerName.text.Length);
        Debug.Log(playerName.text);
        if(playerName.text.Length > 1)
        {
            highscore.playerName = playerName.text;
            SceneManager.LoadScene("Main");
        } else
        {
            please.color = Color.red;
        }

    }
}
