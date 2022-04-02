using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Rigidbody2D rig;
    private GameObject earth;
    public float speed;
    public float angularSpeed;
    public GameObject Explosion;
    // Start is called before the first frame update
    void Start()
    {
        earth = GameObject.FindGameObjectWithTag("Earth");
        rig = GetComponent<Rigidbody2D>();
        rig.velocity = new Vector3(earth.transform.position.x - transform.position.x, earth.transform.position.y - transform.position.y, 0).normalized * speed;
        rig.angularVelocity = Random.value * angularSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnMouseDown()
    {
        Die();
    }

    public void Die()
    {
        rig.velocity = new Vector3(0, 0, 0);
        CircleCollider2D[] colliders = GetComponents<CircleCollider2D>();
        foreach (CircleCollider2D col in colliders)
        {
            col.enabled = false;
        }
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}