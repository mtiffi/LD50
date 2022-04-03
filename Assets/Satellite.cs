using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour
{
    private Shield shield;
    public GameObject Explosion;
    private float comingDownTimer;
    public float frequency;
    public float speed;
    public float chance;
    public SpriteRenderer laserRenderer;
    private enum State
    {
        goingDown,
        laserOn,
        laserOff,
        goingUp,
        waiting
    }
    private State currentState = State.waiting;
    // Start is called before the first frame update
    void Start()
    {
        shield = GetComponentInParent<Shield>();
    }

    private void OnEnable()
    {
        Lifes.onGameOver += Die;
    }

    private void OnDisable()
    {
        Lifes.onGameOver -= Die;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentState == State.waiting)
        {
            comingDownTimer += Time.deltaTime;
            if (comingDownTimer > frequency)
            {
                if (Random.Range(0, 100) < chance)
                {
                    currentState = State.goingDown;
                    Debug.Log("Won");
                }
                else Debug.Log("Lost");
                comingDownTimer = 0;
            }
        }
        if (currentState == State.goingDown)
        {
            if (transform.localPosition.y >= 1.3f)
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - speed, transform.localPosition.z);
            else
            {
                currentState = State.laserOn;
                Invoke("GoBackUp", 3f);
            }
        }

        if (currentState == State.laserOn)
        {
            if (laserRenderer.color.a <= 1)
                laserRenderer.color = new Color(laserRenderer.color.r, laserRenderer.color.g, laserRenderer.color.b, laserRenderer.color.a + .1f);
            else currentState = State.laserOff;
        }
        if (currentState == State.laserOff)
        {
            if (laserRenderer.color.a >= 0)
                laserRenderer.color = new Color(laserRenderer.color.r, laserRenderer.color.g, laserRenderer.color.b, laserRenderer.color.a - .1f);
            else currentState = State.laserOn;
        }
        if(currentState == State.goingUp)
        {
            if (transform.localPosition.y <= 14.8f)
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + speed, transform.localPosition.z);
            else
            {
                currentState = State.waiting;
            }
        }
    }

    private void GoBackUp()
    {
        currentState = State.goingUp;
        laserRenderer.color = new Color(laserRenderer.color.r, laserRenderer.color.g, laserRenderer.color.b, 0);

    }

    private void OnMouseDown()
    {
        Die();
    }

    public void Die()
    {
        CircleCollider2D[] colliders = GetComponents<CircleCollider2D>();
        foreach (CircleCollider2D col in colliders)
        {
            col.enabled = false;
        }
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
        shield.Die();
    }
}
