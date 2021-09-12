using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    GameObject seedCountText;
    GameObject moveLevelText;
    GameObject seedDropRateText;
    GameObject seedEatSpeedText;
    GameObject seedQualityText;
    GameObject risu;

    int seedCount = 0;
    int seedDropRate = 0;
    int risuMoveLevel = 0;
    int eatSpeed = 0;
    int seedQuality = 0;

    public void AddSeed(int count = 1) { seedCount += count;}

    public int getSeedCount() { return seedCount; }
    public void setSeedCount(int set) { seedCount = set; }

    public int getDropRate() { return seedDropRate; }
    public void setDropRate(int set) { seedDropRate = set; }

    public int getMoveLevel() { return risuMoveLevel; }
    public void setMoveLevel(int set) { risuMoveLevel = set; }

    public int getEatSpead() { return eatSpeed; }
    public void setEatSpeed(int set) { eatSpeed = set; }

    public int getSeedQuality() { return seedQuality; }
    public void setSeedQuality(int set) { seedQuality = set; }

    // Start is called before the first frame update
    void Start()
    {
        this.seedCountText = GameObject.Find("Time");
        this.moveLevelText = GameObject.Find("Level");
        this.seedDropRateText = GameObject.Find("Seed");
        this.seedEatSpeedText = GameObject.Find("SeedEat");
        this.seedQualityText = GameObject.Find("Quality");
        this.risu = GameObject.Find("risu");
    }

    // Update is called once per frame
    void Update()
    {
        this.seedCountText.GetComponent<Text>().text = seedCount.ToString() + " Seed";
        this.moveLevelText.GetComponent<Text>().text = "move level : " + this.risuMoveLevel.ToString();
        this.seedDropRateText.GetComponent<Text>().text = "drop Level : " + seedDropRate.ToString();
        this.seedEatSpeedText.GetComponent<Text>().text = "eat speed : " + eatSpeed.ToString();
        this.seedQualityText.GetComponent<Text>().text = "seed quality : " + seedQuality.ToString();
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Additive);
    }
}
