using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    GameObject player;
    float time = 0;
    float lifetime = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = this.player.transform.position;
        transform.position = playerPos;

        if (gameObject.activeSelf)
        {
            this.time += Time.deltaTime;
            if (time > this.lifetime)
            {
                this.time = 0;
                gameObject.SetActive(false);
            }
        }
    }
}
