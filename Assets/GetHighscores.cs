using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetHighscores : MonoBehaviour
{

    public Scores scores;
    private TextMeshProUGUI text;

    private Highscore highscore;
    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("HighscoreList").GetComponent<TextMeshProUGUI>();
        highscore = GameObject.Find("Highscore").GetComponent<Highscore>();
        doPost();

    }

    // Update is called once per frame
    void Update()
    {

    }


    void doPost()
    {
        string URL = "http://localhost:4000/api/highscores";
        string json = "{'name':'" + highscore.playerName + "','highscore':" + highscore.highscore + "}";
        // string json = "{'name':'" + "highscore.name" + "','highscore':" + 1234 + "}";

        string accessToken = "abc";
        Dictionary<string, string> headers = new Dictionary<string, string>();

        // headers.Add("Authorization", "Basic " + accessToken);
        headers.Add("Content-Type", "application/json");

        json = json.Replace("'", "\"");

        Debug.Log(json);
        //Encode the JSON string into a bytes
        byte[] postData = System.Text.Encoding.UTF8.GetBytes(json);
        //Now we call a new WWW request
        WWW www = new WWW(URL, postData, headers);
        //And we start a new co routine in Unity and wait for the response.
        StartCoroutine(WaitForRequest(www));
    }
    //Wait for the www Request
    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        if (www.error == null)
        {
            string highscoreText = "";

            Debug.Log(www.text);
            scores = JsonUtility.FromJson<Scores>(www.text);
            for (int i = 0; i < scores.scores.Length; i++)
            {
                highscoreText += (i + 1) + ". " + scores.scores[i].name + " - " + scores.scores[i].highscore + "\n";
            }
            text.SetText(highscoreText);

            //Print server response
        }
        else
        {
            //Something goes wrong, print the error response
            Debug.Log(www.error);
        }
    }
}
