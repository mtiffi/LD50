using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Rigidbody2D rig;
    private GameObject earth;
    public float speed;
    public float angularSpeed;
    public GameObject Explosion, Smoke;
    private float checkDistanceCounter;
    private Highscore highscore;
    public enum AstroidType
    {
        normal, big, fast, huge
    }

    private int bigLife = 5;
    private int hugeLife = 20;


    public AstroidType currentAstroidType = AstroidType.normal;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        highscore = GameObject.Find("Highscore").GetComponent<Highscore>();

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
        checkDistanceCounter += Time.deltaTime;
        if (checkDistanceCounter > 10)
        {
            if (Vector3.Distance(transform.position, new Vector3(0, 0, 0)) > 30)
            {
                Destroy(gameObject);
            }
            checkDistanceCounter = 0;
        }
    }


    private void OnMouseDown()
    {
        if ((currentAstroidType == AstroidType.big && bigLife > 0) || (currentAstroidType == AstroidType.huge && hugeLife > 0))
        {
            bigLife--;
            hugeLife--;
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(Smoke, new Vector3(touchPos.x, touchPos.y, transform.position.z), Quaternion.identity);
        }
        else
        {
            Die();
        }
    }

    public void Die()
    {
        rig.velocity = new Vector3(0, 0, 0);
        CircleCollider2D[] colliders = GetComponents<CircleCollider2D>();
        foreach (CircleCollider2D col in colliders)
        {
            col.enabled = false;
        }

        GameObject ex = Instantiate(Explosion, transform.position, Quaternion.identity);
        if (currentAstroidType == AstroidType.big || currentAstroidType == AstroidType.huge)
        {
            Transform[] trans = ex.GetComponentsInChildren<Transform>();
            foreach (Transform t in trans)
            {
                if (currentAstroidType == AstroidType.huge)
                    t.localScale = new Vector3(5, 5, 5);
                if (currentAstroidType == AstroidType.big)
                    ex.transform.localScale = new Vector3(2, 2, 2);

            }
        }

        switch(currentAstroidType)
        {
            case AstroidType.normal: highscore.highscore += 10;
                break;
            case AstroidType.big:
                highscore.highscore += 50;
                break;
            case AstroidType.fast:
                highscore.highscore += 500;
                break;
            case AstroidType.huge:
                highscore.highscore += 500;
                break;
        }

        Destroy(gameObject);
    }

}