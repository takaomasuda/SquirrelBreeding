using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject seedPrefab;
    public GameObject goldSeedPrefab;
    float delta = 0;
    float span = 2.0f;

    GameObject director;

    // Start is called before the first frame update
    void Start()
    {
        this.director = GameObject.Find("GameDirector");
    }

    // Update is called once per frame
    void Update()
    {
        int dropRate = this.director.GetComponent<GameDirector>().getDropRate();
        int seedQuality = this.director.GetComponent<GameDirector>().getSeedQuality();

        this.delta += Time.deltaTime;
        if (delta > this.span - (dropRate * 0.2f))
        {
            this.delta = 0;
            GameObject seed;

            int quality = Random.Range(0, 9);
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
