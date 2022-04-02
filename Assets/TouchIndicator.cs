using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchIndicator : MonoBehaviour
{
    public int touchId;
    private Vector3 startPosition;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > touchId)
        {
            Vector3 worldPosition = cam.ScreenToWorldPoint(Input.GetTouch(touchId).position);
            transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);

        }
        else
            transform.position = startPosition;
    }

    
}
