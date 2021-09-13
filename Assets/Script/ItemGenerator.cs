using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject seedPrefab;
    public GameObject goldSeedPrefab;
    float delta = 0;
    float initialSeedDropSpan = 2.0f;

    GameObject director;

    // Start is called before the first frame update
    void Start()
    {
        this.director = GameObject.Find("GameDirector");
    }

    // Update is called once per frame
    void Update()
    {
        int dropSpeed = this.director.GetComponent<GameDirector>().getDropSpeed();
        int seedQuality = this.director.GetComponent<GameDirector>().getSeedQuality();

        this.delta += Time.deltaTime;

        float seedDropSpan = initialSeedDropSpan - (dropSpeed * 0.2f);
        if (dropSpeed >= 10)
        {
            seedDropSpan = 0.15f;
        }

        if (delta > seedDropSpan)
        {
            this.delta = 0;
            GameObject seed;

            int quality = Random.Range(0, 10);
            if (quality < seedQuality)
            {
                seed = Instantiate(goldSeedPrefab) as GameObject;
            }
            else
            {
                seed = Instantiate(seedPrefab) as GameObject;
            }

            int px = Random.Range(-16, 16);
            seed.transform.position = new Vector3(px / 2, 6, 0);
        }
    }
}
