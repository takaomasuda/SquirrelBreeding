using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedController : MonoBehaviour
{
    readonly int dropState = 0;
    readonly int dragState = 1;
    readonly int getableState = 2;

    Vector3 offset;
    int state;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClick()
    {
        state = dragState;
        this.offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnDrag()
    {
        Vector3 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + this.offset;
        transform.position = new Vector3(currentPos.x, currentPos.y, transform.position.z);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    public void OnDrop()
    {
        state = dropState;
    }

    public bool IsGetable()
    {
        bool getable = false;
        if (state == getableState)
        {
            getable = true;
        }
        return getable;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "stage")
        {
            state = getableState;
        }
    }
}
