using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifes : MonoBehaviour
{
    public int lifes = 3;
    private ScreenShake shake;
    public float screenShakeDuration;
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


}
