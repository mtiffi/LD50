using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lifes : MonoBehaviour
{
    public int lifes = 3;
    private ScreenShake shake;
    public float screenShakeDuration;
    public GameObject Explosion;
    public delegate void OnGameOver();
    public static OnGameOver onGameOver;
    // Start is called before the first frame update
    void Start()
    {
        shake = Camera.main.GetComponent<ScreenShake>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public int decreaseLifes(int value)
    {
        lifes -= value;
        if(lifes <= 0)
        {
            for(int i = 1; i < 20; i++)
            {
                Invoke("Explode", i*.5f);

            }
            onGameOver?.Invoke();
            Invoke("GameOver", 5);

        }
        return lifes;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.tag);
        if (collision.collider.tag == "Asteroid")
        {
            decreaseLifes(1);
            collision.gameObject.GetComponent<Asteroid>().Die();
            shake.shakeDuration = screenShakeDuration;
        }
    }

    private Vector3 RandomPointOnCircleEdge(float radius)
    {
        var vector2 = Random.insideUnitCircle.normalized * radius;
        if (vector2.y < 0)
            return RandomPointOnCircleEdge(radius);

        return new Vector3(vector2.x, vector2.y, 0);
    }

    private void GameOver()
    {
        SceneManager.LoadScene("Highscore");
    }

    private void Explode()
    {
        Instantiate(Explosion, transform.position + RandomPointOnCircleEdge(3), Quaternion.identity);
    }

}
