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
    GameObject seedDropSpeedText;
    GameObject eatSpeedText;
    GameObject seedQualityText;

    GameObject moveLevelButton;
    GameObject seedDropSpeedButton;
    GameObject eatSpeedButton;
    GameObject seedQualityButton;

    int moveLevel;
    int seedDropSpeed;
    int eatSpeed;
    int seedQuality;

    int needSeedToUpdateMove;
    int needSeedToUpdateDrop;
    int needSeedToUpdateEat;
    int needSeedToUpdateQuality;

    int maxLevel = 10;

    int calculateUpdateSeed(int level)
    {
        int needSeed = (level + 1) * (level + 1) * 10;
        return needSeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        //オブジェクト取得
        this.seedCountText = GameObject.Find("SeedCount");
        this.moveLevelText = GameObject.Find("RisuMove").transform.GetChild(0).gameObject;
        this.seedDropSpeedText = GameObject.Find("DropSpeed").transform.GetChild(0).gameObject;
        this.eatSpeedText = GameObject.Find("EatSpeed").transform.GetChild(0).gameObject;
        this.seedQualityText = GameObject.Find("SeedQuality").transform.GetChild(0).gameObject;
        this.director = GameObject.Find("GameDirector");

        this.moveLevelButton = GameObject.Find("RisuMove").transform.GetChild(1).gameObject;
        this.seedDropSpeedButton = GameObject.Find("DropSpeed").transform.GetChild(1).gameObject;
        this.eatSpeedButton = GameObject.Find("EatSpeed").transform.GetChild(1).gameObject;
        this.seedQualityButton = GameObject.Find("SeedQuality").transform.GetChild(1).gameObject;

        //各パラメータ取得
        moveLevel = director.GetComponent<GameDirector>().getMoveLevel();
        seedDropSpeed = director.GetComponent<GameDirector>().getDropSpeed();
        eatSpeed = director.GetComponent<GameDirector>().getEatSpead();
        seedQuality = director.GetComponent<GameDirector>().getSeedQuality();

        needSeedToUpdateMove = calculateUpdateSeed(moveLevel);
        needSeedToUpdateDrop = calculateUpdateSeed(seedDropSpeed);
        needSeedToUpdateEat = calculateUpdateSeed(eatSpeed);
        needSeedToUpdateQuality = calculateUpdateSeed(seedQuality);

        //表示更新
        UpdateCurrentSeedIndication();
        UpdateMoveSpeedLevelIndication();
        UpdateDropSpeedLevelIndication();
        UpdateEatSpeedLevelIndication();
        UpdateSeedQualityLevelIndication();
    }

    // 表示更新関数郡
    void UpdateCurrentSeedIndication()
    {
        int seedCount = director.GetComponent<GameDirector>().getSeedCount();
        this.seedCountText.GetComponent<Text>().text = "Seed : " + seedCount.ToString();
    }

    void UpdateMoveSpeedLevelIndication()
    {
        if (moveLevel < this.maxLevel)
        {
            this.moveLevelText.GetComponent<Text>().text =
                "auto move: level " + moveLevel.ToString() + " (update " + needSeedToUpdateMove.ToString() + " seed)";
        }
        else
        {
            this.moveLevelText.GetComponent<Text>().text = "auto move: level max";
            if (this.moveLevelButton.activeSelf) this.moveLevelButton.SetActive(false);
        }
    }

    void UpdateDropSpeedLevelIndication()
    {
        if (seedDropSpeed < this.maxLevel)
        {
            this.seedDropSpeedText.GetComponent<Text>().text =
                "seed drop speed: level " + seedDropSpeed.ToString() + " (update " + needSeedToUpdateDrop.ToString() + " seed)";
        }
        else
        {
            this.seedDropSpeedText.GetComponent<Text>().text = "seed drop speed: level max";
            if (this.seedDropSpeedButton.activeSelf) this.seedDropSpeedButton.SetActive(false);
        }
    }

    void UpdateEatSpeedLevelIndication()
    {
        if (eatSpeed < this.maxLevel)
        {
            this.eatSpeedText.GetComponent<Text>().text =
            "eat speed: level " + eatSpeed.ToString() + " (update " + needSeedToUpdateEat.ToString() + " seed)";
        }
        else
        {
            this.eatSpeedText.GetComponent<Text>().text = "eat speed: level max";
            if (this.eatSpeedButton.activeSelf) this.eatSpeedButton.SetActive(false);
        }
    }

    void UpdateSeedQualityLevelIndication()
    {
        if (seedQuality < this.maxLevel)
        {
            this.seedQualityText.GetComponent<Text>().text =
            "seed quality : level " + seedQuality.ToString() + " (update " + needSeedToUpdateQuality.ToString() + " seed)";
        }
        else
        {
            this.seedQualityText.GetComponent<Text>().text = "speed quality: level max";
            if (this.seedQualityButton.activeSelf) this.seedQualityButton.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        // Seed カウントはバックグランドのSeedの取得を反映する
        UpdateCurrentSeedIndication();
    }

    // MoveSpeed Up のボタン押下時のコールバック
    public void OnUpdateMoveLevel()
    {
        int seedCount = director.GetComponent<GameDirector>().getSeedCount();
        if (seedCount >= needSeedToUpdateMove)
        {
            moveLevel++;
            seedCount -= needSeedToUpdateMove;
            needSeedToUpdateMove = calculateUpdateSeed(moveLevel);
        }
        director.GetComponent<GameDirector>().setSeedCount(seedCount);
        director.GetComponent<GameDirector>().setMoveLevel(moveLevel);

        UpdateCurrentSeedIndication();
        UpdateMoveSpeedLevelIndication();
    }

    // DropSpeed Up のボタン押下時のコールバック
    public void OnUpdateDropRate()
    {
        int seedCount = director.GetComponent<GameDirector>().getSeedCount();
        if (seedCount >= needSeedToUpdateDrop)
        {
            seedDropSpeed++;
            seedCount -= needSeedToUpdateDrop;
            needSeedToUpdateDrop = calculateUpdateSeed(seedDropSpeed);
        }
        director.GetComponent<GameDirector>().setSeedCount(seedCount);
        director.GetComponent<GameDirector>().setDropSpeed(seedDropSpeed);

        UpdateCurrentSeedIndication();
        UpdateDropSpeedLevelIndication();
    }

    // EatSpeed Up のボタン押下時のコールバック
    public void OnUpdteEatSpeed()
    {
        int seedCount = director.GetComponent<GameDirector>().getSeedCount();
        if (seedCount >= needSeedToUpdateEat)
        {
            eatSpeed++;
            seedCount -= needSeedToUpdateEat;
            needSeedToUpdateEat = calculateUpdateSeed(eatSpeed);
        }
        director.GetComponent<GameDirector>().setSeedCount(seedCount);
        director.GetComponent<GameDirector>().setEatSpeed(eatSpeed);

        UpdateCurrentSeedIndication();
        UpdateEatSpeedLevelIndication();
    }

    // SeedQuality Up のボタン押下のコールバック
    public void OnUpdteSeedQuality()
    {
        int seedCount = director.GetComponent<GameDirector>().getSeedCount();
        if (seedCount >= needSeedToUpdateQuality)
        {
            seedQuality++;
            seedCount -= needSeedToUpdateQuality;
            needSeedToUpdateQuality = calculateUpdateSeed(seedQuality);
        }
        director.GetComponent<GameDirector>().setSeedCount(seedCount);
        director.GetComponent<GameDirector>().setSeedQuality(seedQuality);

        UpdateCurrentSeedIndication();
        UpdateSeedQualityLevelIndication();
    }

    // Menu 閉じる
    public void OnClickClose()
    {
        SceneManager.UnloadSceneAsync("MenuScene");
    }
}
