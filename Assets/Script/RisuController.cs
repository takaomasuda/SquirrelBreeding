using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisuController : MonoBehaviour
{
    readonly int idleState = 0;
    readonly int runState = 1;
    readonly int eatState = 2;

    GameObject director;
    Animator anime;
    int state;

    GameObject SearchTargetSeed()
    {
        string[] SeedTypeList = new string[]
        {
            "Seed",
            "goldSeed"
        };

        GameObject target = null;
        float nearestDistance = -1;
        foreach (string SeedType in SeedTypeList)
        {
            var Seeds = GameObject.FindGameObjectsWithTag(SeedType);
            foreach (GameObject seed in Seeds)
            {
                if (!seed.GetComponent<SeedController>().IsGetable())
                {
                    continue;
                }
                float distance = Vector3.Distance(seed.transform.position, transform.position);
                if (nearestDistance == -1 ||
                    nearestDistance > distance)
                {
                    nearestDistance = distance;
                    target = seed;
                }
            }
        }
        return target;
    }

    void SetAnimeState(int state)
    {
        this.state = state;
        this.anime.SetInteger("State", this.state);

        if (this.state == idleState)
        {
            this.anime.speed = 1.0f;
        }
        else if (this.state == runState)
        {
            int moveLevel = this.director.GetComponent<GameDirector>().getMoveLevel();
            this.anime.speed = moveLevel * 0.5f;
        }
        else if (this.state == eatState)
        {
            int eatSpeed = this.director.GetComponent<GameDirector>().getEatSpead();
            this.anime.speed = 0.5f + (eatSpeed * 0.5f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.director = GameObject.Find("GameDirector");
        this.anime = GetComponent<Animator>();
        SetAnimeState(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        int lastState = this.state;
        int moveLevel = this.director.GetComponent<GameDirector>().getMoveLevel();

        GameObject targetSeed = SearchTargetSeed();

        if (this.state == eatState)
        {
            AnimatorStateInfo animeInfo = this.anime.GetCurrentAnimatorStateInfo(0);
            if (animeInfo.IsName("Eat") &&
                animeInfo.normalizedTime >= 1)
            {
                SetAnimeState(idleState);
            }
        }
        else if (moveLevel > 0 &&
                  targetSeed != null)
        {
            SetAnimeState(runState);
        }
        else
        {
            SetAnimeState(idleState);
        }

        if (lastState == runState && this.state != eatState)
        {
            float speed = moveLevel * Time.deltaTime;
            float dir = targetSeed.transform.position.x - transform.position.x;
            if (dir == 0)
            {
                SetAnimeState(idleState);
            }
            else if (Mathf.Abs(dir) < speed)
            {
                transform.Translate(new Vector3(dir, 0, 0));
            }
            else if (dir > 0)
            {
                transform.Translate(new Vector3(speed, 0, 0));
            }
            else // dir < 0
            {
                transform.Translate(new Vector3(-1 * speed, 0, 0));
            }

            if (dir < 0)
            {
                transform.localScale = new Vector3(4, 4, 1);
            }
            else
            {
                transform.localScale = new Vector3(-4, 4, 1);

            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (this.state != eatState)
        {
            if (collision.gameObject.tag == "Seed")
            {
                this.director.GetComponent<GameDirector>().AddSeed();
                GameObject.Destroy(collision.gameObject);
                SetAnimeState(eatState);
            }
            else if (collision.gameObject.tag == "goldSeed")
            {
                this.director.GetComponent<GameDirector>().AddSeed(10);
                GameObject.Destroy(collision.gameObject);
                SetAnimeState(eatState);
            }
        }
    }
}
