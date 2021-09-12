using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    GameObject director;
    public GameObject enemyPrefab;
    float delta = 0;
    float span = 1f;
    float speed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        this.director = GameObject.Find("GameDirector");
    }

    // Update is called once per frame
    void Update()
    {
        int level = 1;
        this.delta += Time.deltaTime;
        if(delta > this.span / level)
        {
            this.delta = 0;
            GameObject enemy = Instantiate(enemyPrefab) as GameObject;
            int dir = Random.Range(0, 3);
            if(dir == 0)
            {
                int py = Random.Range(-4, 6);
                enemy.transform.position = new Vector3(11, py, 0);
                int vx = Random.Range(1, 2);
                enemy.GetComponent<EnemyController>().SetVelocity(new Vector3(-1 * vx * this.speed * level, 0, 0));
            }
            else if(dir == 1)
            {
                int px = Random.Range(-9, 16);
                enemy.transform.position = new Vector3(px, 6, 0);
                int vy = Random.Range(1, 2);
                enemy.GetComponent<EnemyController>().SetVelocity(new Vector3(0, -1 * vy * this.speed * level, 0));
            }
            else
            {
                int py = Random.Range(-4, 6);
                enemy.transform.position = new Vector3(-11, py, 0);
                int vx = Random.Range(1, 2);
                enemy.GetComponent<EnemyController>().SetVelocity(new Vector3(vx * this.speed * level, 0, 0));
            }
        }
    }
}
