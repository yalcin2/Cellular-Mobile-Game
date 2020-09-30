using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//////////////////////////////////////////////////
//
// THIS SCRIPT CONTAINS THE STATIC VARIABLES USED IN
// ALL THE OTHER SCRIPTS. SaveGame() AND OnEnable() ARE 
// USED TO SAVE AND LOAD THE DATA WITH THE USE OF
// PLAYERPREFS. SetDefaultSettings() SHOULD BE USED ONLY
// TO RESET THE GAME DATA.
//
// AUTHOR: YALCIN FORMOSA
//////////////////////////////////////////////////




public class ValuesScript : MonoBehaviour
{

    ///////////////// GAME & SCORE

    public static int coinScore;
    public static int availableCoins;

    public static string newPlayer;

    ///////////////// PLAYER

    public static int playerHealth;
    public static float playerSize;
    public static string maxSizeReached;
    public static float playerSpeedMultiplier;

    ///////////////// LEVEL

    public static int currentLevel;
    public static float expToRank;
    public static float playerExp;
    public static int levelPoints;

    ///////////////// UPGRADES

    // UPGRADE 1
    public static float cellsSpawnTime;
    public static int cellsSpawnUpgradeLevel;

    // UPGRADE 2
    public static int cellsToSpawn;
    public static int amountOfCellsUpgradeLevel;

    // UPGRADE 3



    ///////////////// SKILLS

    // SKILL 1
    public static string magnetUnlocked;
    public static int magnetPower;
    public static int magnetRange;

    // SKILL 2
    public static string fastestCellSpawnUnlocked;

    // SKILL 3




    public Image levelBar;

    public Text currentRank;
    public Text playerScore;
    public Text playerSecondaryScore;

    // Start is called before the first frame update
    void Start()
    {
        availableCoins = 0;
        playerSpeedMultiplier = 2f;

        if (newPlayer == null)
        {
            setDefaultSettings();
            newPlayer = "no";
        }


        print(cellsToSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        saveGame();

        playerScore.text = coinScore.ToString();
        playerSecondaryScore.text = coinScore.ToString();
        currentRank.text = currentLevel.ToString();

        levelBar.fillAmount = playerExp;

        if (levelBar.fillAmount == 1)
        {
            levelBar.fillAmount = 50000;
            playerExp = 0;
            currentLevel = currentLevel + 1;
            levelPoints = levelPoints + 1;
            LevelScript.sizeIncreased = true;
            currentRank.text = currentLevel.ToString();
        }

        if (availableCoins >= cellsToSpawn) {
            ConsumableScript.canSpawn = false;
        }
        if (availableCoins < cellsToSpawn)
        {
            ConsumableScript.canSpawn = true;
        }
    }

    // START COINS 2500

    public void setDefaultSettings()
    {
        coinScore = 50000; //test
        availableCoins = 0;
        cellsToSpawn = 150;
        playerHealth = 1;
        playerSize = 1;
        maxSizeReached = "no";
        playerSpeedMultiplier = 2f; //test
        currentLevel = 1;
        expToRank = 0.1f;
        playerExp = 0f;
        cellsSpawnTime = 3f;
        cellsSpawnUpgradeLevel = 0;
        amountOfCellsUpgradeLevel = 0;
        magnetUnlocked = "no";
        magnetPower = 1;
        magnetRange = 1;
        levelPoints = 50; //test
        fastestCellSpawnUnlocked = "no";
    }



    void saveGame() {
        PlayerPrefs.SetInt("coinScore", coinScore);
        PlayerPrefs.SetInt("cellsToSpawn", cellsToSpawn);
        PlayerPrefs.SetInt("playerHealth", playerHealth);
        PlayerPrefs.SetFloat("playerSize", playerSize);
        PlayerPrefs.SetFloat("playerSpeedMultiplier", playerSpeedMultiplier);
        PlayerPrefs.SetInt("currentLevel", currentLevel);
        PlayerPrefs.SetFloat("expToRank", expToRank);
        PlayerPrefs.SetFloat("playerExp", playerExp);
        PlayerPrefs.SetString("newPlayer", newPlayer);
        PlayerPrefs.SetString("maxSizeReached", maxSizeReached);
        PlayerPrefs.SetFloat("cellsSpawnTime", cellsSpawnTime);
        PlayerPrefs.SetInt("cellsSpawnUpgradeLevel", cellsSpawnUpgradeLevel);
        PlayerPrefs.SetInt("amountOfCellsUpgradeLevel", amountOfCellsUpgradeLevel);
        PlayerPrefs.SetString("magnetUnlocked", magnetUnlocked);
        PlayerPrefs.SetInt("magnetPower", magnetPower);
        PlayerPrefs.SetInt("levelPoints", levelPoints);
        PlayerPrefs.SetString("fastestCellSpawnUnlocked", fastestCellSpawnUnlocked);
        PlayerPrefs.SetInt("magnetRange", magnetRange);

        PlayerPrefs.Save();
        print(PlayerPrefs.GetInt("coinScore"));
    }

    void OnEnable()
    {
        coinScore = PlayerPrefs.GetInt("coinScore");
        cellsToSpawn = PlayerPrefs.GetInt("cellsToSpawn");
        playerHealth = PlayerPrefs.GetInt("playerHealth");
        playerSize = PlayerPrefs.GetFloat("playerSize");
        playerSpeedMultiplier = PlayerPrefs.GetInt("playerSpeedMultiplier");
        currentLevel = PlayerPrefs.GetInt("currentLevel");
        expToRank = PlayerPrefs.GetFloat("expToRank");
        playerExp = PlayerPrefs.GetFloat("playerExp");
        newPlayer = PlayerPrefs.GetString("newPlayer");
        maxSizeReached = PlayerPrefs.GetString("maxSizeReached");
        cellsSpawnTime = PlayerPrefs.GetFloat("cellsSpawnTime");
        cellsSpawnUpgradeLevel = PlayerPrefs.GetInt("cellsSpawnUpgradeLevel");
        amountOfCellsUpgradeLevel = PlayerPrefs.GetInt("amountOfCellsUpgradeLevel");
        magnetUnlocked = PlayerPrefs.GetString("magnetUnlocked");
        magnetPower = PlayerPrefs.GetInt("magnetPower");
        levelPoints = PlayerPrefs.GetInt("levelPoints");
        fastestCellSpawnUnlocked = PlayerPrefs.GetString("fastestCellSpawnUnlocked");
        magnetRange = PlayerPrefs.GetInt("magnetRange");
    }

    void OnApplicationQuit()
    {
        saveGame();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
            saveGame();
    }
}
