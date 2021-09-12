using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    GameObject director;

    GameObject seedCountText;
    GameObject moveLevelText;
    GameObject seedDropRateText;
    GameObject eatSpeedText;
    GameObject seedQualityText;

    int seedCount;
    int moveLevel;
    int seedDropRate;
    int eatSpeed;
    int seedQuality;

    int needSeedToUpdateMove;
    int needSeedToUpdateDrop;
    int needSeedToUpdateEat;
    int needSeedToUpdateQuality;

    int calculateUpdateSeed(int level)
    {
        int needSeed = (level + 1) * (level + 1) * 10;
        return needSeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.seedCountText = GameObject.Find("SeedCount");
        this.moveLevelText = GameObject.Find("RisuMove").transform.GetChild(0).gameObject;
        this.seedDropRateText = GameObject.Find("DropSpeed").transform.GetChild(0).gameObject;
        this.eatSpeedText = GameObject.Find("EatSpeed").transform.GetChild(0).gameObject;
        this.seedQualityText = GameObject.Find("SeedQuality").transform.GetChild(0).gameObject;

        director = GameObject.Find("GameDirector");

        seedCount = director.GetComponent<GameDirector>().getSeedCount();
        moveLevel = director.GetComponent<GameDirector>().getMoveLevel();
        seedDropRate = director.GetComponent<GameDirector>().getDropRate();
        eatSpeed = director.GetComponent<GameDirector>().getEatSpead();
        seedQuality = director.GetComponent<GameDirector>().getSeedQuality();

        needSeedToUpdateMove = calculateUpdateSeed(moveLevel);
        needSeedToUpdateDrop = calculateUpdateSeed(seedDropRate);
        needSeedToUpdateEat = calculateUpdateSeed(eatSpeed);
        needSeedToUpdateQuality = calculateUpdateSeed(seedQuality);
    }

    // Update is called once per frame
    void Update()
    {
        this.seedCountText.GetComponent<Text>().text = "Seed : " + seedCount.ToString();

        this.moveLevelText.GetComponent<Text>().text =
            "risu auto move: level " + moveLevel.ToString() + " (update " + needSeedToUpdateMove.ToString() + " seed)";

        this.seedDropRateText.GetComponent<Text>().text =
            "Seed drop speed: level " + seedDropRate.ToString() + " (update " + needSeedToUpdateDrop.ToString() + " seed)";

        this.eatSpeedText.GetComponent<Text>().text =
            "eat speed: level " + eatSpeed.ToString() + " (update " + needSeedToUpdateEat.ToString() + " seed)";

        this.seedQualityText.GetComponent<Text>().text =
            "seed quality : level " + seedQuality.ToString() + " (update " + needSeedToUpdateQuality.ToString() + " seed)";

    }

    public void OnUpdateMoveLevel()
    {
        if (seedCount >= needSeedToUpdateMove)
        {
            moveLevel++;
            seedCount -= needSeedToUpdateMove;
            needSeedToUpdateMove = calculateUpdateSeed(moveLevel);
        }
    }

    public void OnUpdateDropRate()
    {
        if (seedCount >= needSeedToUpdateDrop)
        {
            seedDropRate++;
            seedCount -= needSeedToUpdateDrop;
            needSeedToUpdateDrop = calculateUpdateSeed(seedDropRate);
        }
    }
    public void OnUpdteEatSpeed()
    {
        if (seedCount >= needSeedToUpdateEat)
        {
            eatSpeed++;
            seedCount -= needSeedToUpdateEat;
            needSeedToUpdateEat = calculateUpdateSeed(eatSpeed);
        }
    }
    public void OnUpdteSeedQuality()
    {
        if (seedCount >= needSeedToUpdateQuality)
        {
            seedQuality++;
            seedCount -= needSeedToUpdateQuality;
            needSeedToUpdateQuality = calculateUpdateSeed(seedQuality);
        }
    }

    public void OnClickClose()
    {
        director.GetComponent<GameDirector>().setSeedCount(seedCount);
        director.GetComponent<GameDirector>().setMoveLevel(moveLevel);
        director.GetComponent<GameDirector>().setDropRate(seedDropRate);
        director.GetComponent<GameDirector>().setEatSpeed(eatSpeed);
        director.GetComponent<GameDirector>().setSeedQuality(seedQuality);

        SceneManager.UnloadSceneAsync("MenuScene");
    }
}
