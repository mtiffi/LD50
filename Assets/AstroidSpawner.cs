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
    // Start is called before the first frame update
    void Start()
    {
        Earth = GameObject.FindGameObjectWithTag("Earth");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnTime)
        {
            spawnTimer = 0;
            Instantiate(astroid, Earth.transform.position + RandomPointOnCircleEdge(spawnRadius), Quaternion.identity);

        }
    }

    private Vector3 RandomPointOnCircleEdge(float radius)
    {
        var vector2 = Random.insideUnitCircle.normalized * radius;
        if (vector2.y < yBorder)
            return RandomPointOnCircleEdge(radius);

        return new Vector3(vector2.x, vector2.y, 0);
    }
}
