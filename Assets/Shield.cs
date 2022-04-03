using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Shield : MonoBehaviour
{
    private ScreenShake shake;
    public float screenShakeDuration;
    private Light2D light2D;
    private bool up = true;
    public float lightFlickerSpeed;
    private AudioSource audio;


    // Start is called before the first frame update
    void Start()
    {
        shake = Camera.main.GetComponent<ScreenShake>();
        light2D = GetComponentInChildren<Light2D>();
        audio = Camera.main.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Flicker(); 

    }

    void Flicker()
    {
        if (light2D.intensity < 7 && up)
        {
            light2D.intensity += lightFlickerSpeed;
        }

        if (light2D.intensity >= 7 && up)
        {
            up = false;
        }

        if (light2D.intensity > 4f && !up)
        {
            light2D.intensity -= lightFlickerSpeed;
        }
        if (light2D.intensity <= 4f && !up)
        {
            up = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.tag);
        if (collision.collider.tag == "Asteroid")
        {
            collision.gameObject.GetComponent<Asteroid>().Die();
            Die();
        }
    }

    public void Die()
    {
        shake.shakeDuration = screenShakeDuration;
        GetComponent<PolygonCollider2D>().enabled = false;
        lightFlickerSpeed = .2f;
        audio.Play();

        Invoke("DestroyMe", 2f);

    }

    void DestroyMe()
    {
        Destroy(gameObject);

    }
}
