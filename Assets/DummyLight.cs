using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyLight : MonoBehaviour
{
    private bool right;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (right)
            transform.position = new Vector3(transform.position.x + .1f, transform.position.y, 0);
        else
            transform.position = new Vector3(transform.position.x - .1f, transform.position.y, 0);
        if (transform.position.x < -10)
            right = true;
        if (transform.position.x > 10)
            right = false;
    }
}
