using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ExplosionLight : MonoBehaviour
{
    private Light2D light2D;
    bool darker;
 

    // Start is called before the first frame update
    void Start()
    {
        light2D = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        light2D.intensity -= 0.03f;
    }
}
