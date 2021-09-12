using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Vector3 velocity;
    public void SetVelocity(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocity);
        if(transform.position.x < -11 ||
            transform.position.x > 11 ||
            transform.position.y < -6)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
           Destroy(gameObject);
        }
    }
}
