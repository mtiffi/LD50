using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidSpawner : MonoBehaviour
{

    private float spawnTimer;
    public GameObject astroid;
    public float spawnTime;
    public float spawnRadius;
    private GameObject Earth;
    public float yBorder;
    private bool spawn = true;
    public float speed;
    public float angularSpeed;
    private float chanceNormal = 20, chanceBig = 0, chanceFast = 0, chanceHuge = 0, chanceMiss = 80;
    private Highscore highscore;

    public enum Level
    {
        easy,
        medium,
        hard,
        harder,
        bossmode, insane
    }

    public Level currentLevel = Level.easy;

    private void OnEnable()
    {
        Lifes.onGameOver += DeactivateSpawner;
    }

    private void OnDisable()
    {
        Lifes.onGameOver -= DeactivateSpawner;
    }

    // Start is called before the first frame update
    void Start()
    {
        Earth = GameObject.FindGameObjectWithTag("Earth");
        highscore = GameObject.Find("Highscore").GetComponent<Highscore>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!spawn)
            return;

        if (highscore.highscore > 50)
            currentLevel = Level.medium;
        if (highscore.highscore > 200)
            currentLevel = Level.hard;
        if (highscore.highscore > 500)
            currentLevel = Level.harder;
        if (highscore.highscore > 2000)
            currentLevel = Level.bossmode;
        if (highscore.highscore > 4000)
            currentLevel = Level.insane;

        switch (currentLevel)
        {
            case Level.medium: spawnTime = 2;
                chanceNormal = 29; chanceBig = 10; chanceFast = 1; chanceHuge = 0; chanceMiss = 60;
                break;
            case Level.hard: spawnTime = 2;
                chanceNormal = 40; chanceBig = 20; chanceFast = 5; chanceHuge = 0; chanceMiss = 35;
                break;
            case Level.harder: spawnTime = 1.5f;
                chanceNormal = 20; chanceBig = 20; chanceFast = 10; chanceHuge = 10; chanceMiss = 40;
                break;
            case Level.bossmode: spawnTime = 1f;
                chanceNormal = 20; chanceBig = 20; chanceFast = 10; chanceHuge = 10; chanceMiss = 40;
                break;
            case Level.insane: spawnTime = .5f;
                chanceNormal = 0; chanceBig = 0; chanceFast = 100; chanceHuge = 0; chanceMiss = 0;
                break;
        }

        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnTime)
        {
            spawnTimer = 0;
            float random = Random.Range(1, 101);
            random -= chanceNormal;
            if (random < 0)
            {
                SpawnSimple();
                return;
            }
            random -= chanceMiss;
            if (random < 0)
            {
                SpawnMissing();
                return;
            }
            random -= chanceBig;
            if (random < 0)
            {
                SpawnBig();
                return;
            }
            random -= chanceHuge;
            if (random < 0)
            {
                SpawnHuge();
                return;
            }
            random -= chanceFast;
            if (random < 0)
            {
                SpawnFast();
                return;
            }
        }
    }

    void SpawnSimple()
    {
        GameObject instance = Instantiate(astroid, Earth.transform.position + RandomPointOnCircleEdge(spawnRadius, false), Quaternion.identity);
        Rigidbody2D rig = instance.GetComponent<Rigidbody2D>();
        rig = instance.GetComponent<Rigidbody2D>();
        rig.velocity = new Vector3(Earth.transform.position.x - instance.transform.position.x, Earth.transform.position.y - instance.transform.position.y, 0).normalized * speed;
        rig.angularVelocity = Random.value * angularSpeed;
    }

    void SpawnFast()
    {
        GameObject instance = Instantiate(astroid, Earth.transform.position + RandomPointOnCircleEdge(spawnRadius, false), Quaternion.identity);
        instance.transform.localScale = new Vector3(.5f, .5f, .5f); 
        instance.GetComponent<Asteroid>().currentAstroidType = Asteroid.AstroidType.fast;
        instance.GetComponent<SpriteRenderer>().color = Color.red;
        Rigidbody2D rig = instance.GetComponent<Rigidbody2D>();
        rig = instance.GetComponent<Rigidbody2D>();
        rig.velocity = new Vector3(Earth.transform.position.x - instance.transform.position.x, Earth.transform.position.y - instance.transform.position.y, 0).normalized * speed*2;
        rig.angularVelocity = Random.value * angularSpeed;
    }

    void SpawnBig()
    {
        GameObject instance = Instantiate(astroid, Earth.transform.position + RandomPointOnCircleEdge(spawnRadius, false), Quaternion.identity);
        instance.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        instance.GetComponent<Asteroid>().currentAstroidType = Asteroid.AstroidType.big;
        Rigidbody2D rig = instance.GetComponent<Rigidbody2D>();
        rig = instance.GetComponent<Rigidbody2D>();
        rig.velocity = new Vector3(Earth.transform.position.x - instance.transform.position.x, Earth.transform.position.y - instance.transform.position.y, 0).normalized * speed/3;
        rig.angularVelocity = Random.value * angularSpeed;
    }

    void SpawnHuge()
    {
        GameObject instance = Instantiate(astroid, Earth.transform.position + RandomPointOnCircleEdge(spawnRadius, false), Quaternion.identity);
        instance.transform.localScale = new Vector3(3f, 3f, 3f);
        instance.GetComponent<Asteroid>().currentAstroidType = Asteroid.AstroidType.huge;
        Rigidbody2D rig = instance.GetComponent<Rigidbody2D>();
        rig = instance.GetComponent<Rigidbody2D>();
        rig.velocity = new Vector3(Earth.transform.position.x - instance.transform.position.x, Earth.transform.position.y - instance.transform.position.y, 0).normalized * speed / 5;
        rig.angularVelocity = Random.value * angularSpeed;
    }

    void SpawnMissing()
    {
        GameObject instance = Instantiate(astroid, Earth.transform.position + RandomPointOnCircleEdge(spawnRadius, false), Quaternion.identity);
        Rigidbody2D rig = instance.GetComponent<Rigidbody2D>();
        rig = instance.GetComponent<Rigidbody2D>();
        Vector3 target = RandomPointOnCircleEdge(Random.Range(5, 10), false);
        rig.velocity = new Vector3(target.x - instance.transform.position.x, target.y - instance.transform.position.y, 0).normalized * speed;
        rig.angularVelocity = Random.value * angularSpeed;
    }

    void DeactivateSpawner()
    {
        spawn = false;
    }

    private Vector3 RandomPointOnCircleEdge(float radius, bool fullCircle)
    {
        var vector2 = Random.insideUnitCircle.normalized * radius;
        if (vector2.y < yBorder && !fullCircle)
            return RandomPointOnCircleEdge(radius, false);

        return new Vector3(vector2.x, vector2.y, 0);
    }
}
