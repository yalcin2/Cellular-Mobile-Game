using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeShopScript : MonoBehaviour
{

    public GameObject shop_interface;

    public Button upgradeOneBtn;
    public Button upgradeTwoBtn;

    public Text upgradeOnePriceText;
    public Text upgradeTwoPriceText;

    private int UpgradeOnePrice;
    private int UpgradeTwoPrice;

    public Text cellsSpawnlevel;
    public Text cellsToSpawnlevel;

    // Start is called before the first frame update
    void Start()
    {
        shop_interface.SetActive(false);
        UpgradeOnePrice = 40;
        UpgradeTwoPrice = 60;
    }

    // Update is called once per frame
    void Update()
    {
        upgradeOnePriceText.text = UpgradeOnePrice.ToString();
        upgradeTwoPriceText.text = UpgradeTwoPrice.ToString();

        if (ValuesScript.cellsSpawnUpgradeLevel == 10) {
            cellsSpawnlevel.text = "MAX";
        }
        else if (ValuesScript.cellsSpawnUpgradeLevel < 10) {
            cellsSpawnlevel.text = ValuesScript.cellsSpawnUpgradeLevel.ToString();
        }

        if (ValuesScript.amountOfCellsUpgradeLevel == 30)
        {
            cellsToSpawnlevel.text = "MAX";
        }
        else if (ValuesScript.amountOfCellsUpgradeLevel < 30)
        {
            cellsToSpawnlevel.text = ValuesScript.amountOfCellsUpgradeLevel.ToString();
        }

        if (ValuesScript.cellsSpawnUpgradeLevel == 0)
        {
            UpgradeOnePrice = 40;
        }

        else if (ValuesScript.cellsSpawnUpgradeLevel != 0)
        {
            UpgradeOnePrice = (ValuesScript.cellsSpawnUpgradeLevel + 1) * 40;
            upgradeOnePriceText.text = UpgradeOnePrice.ToString();
        }

        if (ValuesScript.cellsSpawnUpgradeLevel == 10)
        {
            upgradeOneBtn.interactable = false;
        }
        else if (ValuesScript.cellsSpawnUpgradeLevel < 10)
        {
            upgradeOneBtn.interactable = true;
        }


        if (ValuesScript.amountOfCellsUpgradeLevel == 0)
        {
            UpgradeTwoPrice = 60;
        }

        else if (ValuesScript.amountOfCellsUpgradeLevel != 0)
        {
            UpgradeTwoPrice = (ValuesScript.amountOfCellsUpgradeLevel + 1) * 60;
            upgradeTwoPriceText.text = UpgradeTwoPrice.ToString();
        }

        if (ValuesScript.amountOfCellsUpgradeLevel == 30)
        {
            upgradeTwoBtn.interactable = false;
        }
        else if (ValuesScript.amountOfCellsUpgradeLevel < 30)
        {
            upgradeTwoBtn.interactable = true;
        }
    }

    public void onShopBtnPressed() {
        shop_interface.SetActive(true);
        Time.timeScale = 0f;
    }

    public void onBackBtnPressed() {
        shop_interface.SetActive(false);
        Time.timeScale = 1f;
    }

    // FIRST UPGRADE

    public void addConsumablesSpawnFasterUpgrade()
    {
        if (ValuesScript.coinScore >= UpgradeOnePrice) {
            if (ValuesScript.cellsSpawnUpgradeLevel == 0) {
                ValuesScript.cellsSpawnTime = 2.7f;
                ValuesScript.cellsSpawnUpgradeLevel = 1;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeOnePrice;
            }
            else if (ValuesScript.cellsSpawnUpgradeLevel == 1)
            {
                ValuesScript.cellsSpawnTime = 2.4f;
                ValuesScript.cellsSpawnUpgradeLevel = 2;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeOnePrice;
            }
            else if (ValuesScript.cellsSpawnUpgradeLevel == 2)
            {
                ValuesScript.cellsSpawnTime = 2.1f;
                ValuesScript.cellsSpawnUpgradeLevel = 3;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeOnePrice;
            }
            else if (ValuesScript.cellsSpawnUpgradeLevel == 3)
            {
                ValuesScript.cellsSpawnTime = 1.8f;
                ValuesScript.cellsSpawnUpgradeLevel = 4;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeOnePrice;
            }
            else if (ValuesScript.cellsSpawnUpgradeLevel == 4)
            {
                ValuesScript.cellsSpawnTime = 1.5f;
                ValuesScript.cellsSpawnUpgradeLevel = 5;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeOnePrice;
            }
            else if (ValuesScript.cellsSpawnUpgradeLevel == 5)
            {
                ValuesScript.cellsSpawnTime = 1.2f;
                ValuesScript.cellsSpawnUpgradeLevel = 6;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeOnePrice;
            }
            else if (ValuesScript.cellsSpawnUpgradeLevel == 6)
            {
                ValuesScript.cellsSpawnTime = 0.9f;
                ValuesScript.cellsSpawnUpgradeLevel = 7;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeOnePrice;
            }
            else if (ValuesScript.cellsSpawnUpgradeLevel == 7)
            {
                ValuesScript.cellsSpawnTime = 0.6f;
                ValuesScript.cellsSpawnUpgradeLevel = 8;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeOnePrice;
            }
            else if (ValuesScript.cellsSpawnUpgradeLevel == 8)
            {
                ValuesScript.cellsSpawnTime = 0.3f;
                ValuesScript.cellsSpawnUpgradeLevel = 9;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeOnePrice;
            }
            else if (ValuesScript.cellsSpawnUpgradeLevel == 9)
            {
                ValuesScript.cellsSpawnTime = 0f;
                ValuesScript.cellsSpawnUpgradeLevel = 10;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeOnePrice;
            }
        }
        else if (ValuesScript.coinScore < UpgradeOnePrice) {
            print("Not enough points");
        }
    }

    /*
    public void removeConsumablesSpawnFasterUpgrade()
    {
        if (ValuesScript.cellsSpawnUpgradeLevel != 0)
        {
            if (ValuesScript.cellsSpawnUpgradeLevel == 1)
            {
                ValuesScript.cellsSpawnTime = 3f;
                ValuesScript.cellsSpawnUpgradeLevel = 0;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeOnePrice - 30;
            }
            else if (ValuesScript.cellsSpawnUpgradeLevel == 2)
            {
                ValuesScript.cellsSpawnTime = 2.7f;
                ValuesScript.cellsSpawnUpgradeLevel = 1;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeOnePrice - 30;
            }
            else if (ValuesScript.cellsSpawnUpgradeLevel == 3)
            {
                ValuesScript.cellsSpawnTime = 2.4f;
                ValuesScript.cellsSpawnUpgradeLevel = 2;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeOnePrice - 30;
            }
            else if (ValuesScript.cellsSpawnUpgradeLevel == 4)
            {
                ValuesScript.cellsSpawnTime = 2.1f;
                ValuesScript.cellsSpawnUpgradeLevel = 3;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeOnePrice - 30;
            }
            else if (ValuesScript.cellsSpawnUpgradeLevel == 5)
            {
                ValuesScript.cellsSpawnTime = 1.8f;
                ValuesScript.cellsSpawnUpgradeLevel = 4;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeOnePrice - 30;
            }
            else if (ValuesScript.cellsSpawnUpgradeLevel == 6)
            {
                ValuesScript.cellsSpawnTime = 1.5f;
                ValuesScript.cellsSpawnUpgradeLevel = 5;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeOnePrice - 30;
            }
            else if (ValuesScript.cellsSpawnUpgradeLevel == 7)
            {
                ValuesScript.cellsSpawnTime = 1.2f;
                ValuesScript.cellsSpawnUpgradeLevel = 6;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeOnePrice - 30;
            }
            else if (ValuesScript.cellsSpawnUpgradeLevel == 8)
            {
                ValuesScript.cellsSpawnTime = 0.9f;
                ValuesScript.cellsSpawnUpgradeLevel = 7;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeOnePrice - 30;
            }
            else if (ValuesScript.cellsSpawnUpgradeLevel == 9)
            {
                ValuesScript.cellsSpawnTime = 0.6f;
                ValuesScript.cellsSpawnUpgradeLevel = 8;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeOnePrice - 30;
            }
            else if (ValuesScript.cellsSpawnUpgradeLevel == 10)
            {
                ValuesScript.cellsSpawnTime = 0.3f;
                ValuesScript.cellsSpawnUpgradeLevel = 9;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeOnePrice - 30;
            }
            
        }
        else if (ValuesScript.coinScore < 30)
        {
            print("Not enough points");
        }
    }
    */

    // SECOND UPGRADE

    public void addMoreCellsUpgrade()
    {
        
        if (ValuesScript.coinScore >= UpgradeTwoPrice)
        {
            if (ValuesScript.amountOfCellsUpgradeLevel == 0)
            {
                ValuesScript.cellsToSpawn = 200;
                ValuesScript.amountOfCellsUpgradeLevel = 1;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 1)
            {
                ValuesScript.cellsToSpawn = 250;
                ValuesScript.amountOfCellsUpgradeLevel = 2;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 2)
            {
                ValuesScript.cellsToSpawn = 300;
                ValuesScript.amountOfCellsUpgradeLevel = 3;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 3)
            {
                ValuesScript.cellsToSpawn = 350;
                ValuesScript.amountOfCellsUpgradeLevel = 4;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 4)
            {
                ValuesScript.cellsToSpawn = 400;
                ValuesScript.amountOfCellsUpgradeLevel = 5;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 5)
            {
                ValuesScript.cellsToSpawn = 450;
                ValuesScript.amountOfCellsUpgradeLevel = 6;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 6)
            {
                ValuesScript.cellsToSpawn = 500;
                ValuesScript.amountOfCellsUpgradeLevel = 7;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 7)
            {
                ValuesScript.cellsToSpawn = 550;
                ValuesScript.amountOfCellsUpgradeLevel = 8;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 8)
            {
                ValuesScript.cellsToSpawn = 600;
                ValuesScript.amountOfCellsUpgradeLevel = 9;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 9)
            {
                ValuesScript.cellsToSpawn = 700;
                ValuesScript.amountOfCellsUpgradeLevel = 10;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 10)
            {
                ValuesScript.cellsToSpawn = 800;
                ValuesScript.amountOfCellsUpgradeLevel = 11;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 11)
            {
                ValuesScript.cellsToSpawn = 900;
                ValuesScript.amountOfCellsUpgradeLevel = 12;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 12)
            {
                ValuesScript.cellsToSpawn = 1000;
                ValuesScript.amountOfCellsUpgradeLevel = 13;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 13)
            {
                ValuesScript.cellsToSpawn = 1100;
                ValuesScript.amountOfCellsUpgradeLevel = 14;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 14)
            {
                ValuesScript.cellsToSpawn = 1200;
                ValuesScript.amountOfCellsUpgradeLevel = 15;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 15)
            {
                ValuesScript.cellsToSpawn = 1300;
                ValuesScript.amountOfCellsUpgradeLevel = 16;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 16)
            {
                ValuesScript.cellsToSpawn = 1400;
                ValuesScript.amountOfCellsUpgradeLevel = 17;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 17)
            {
                ValuesScript.cellsToSpawn = 1500;
                ValuesScript.amountOfCellsUpgradeLevel = 18;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 18)
            {
                ValuesScript.cellsToSpawn = 1600;
                ValuesScript.amountOfCellsUpgradeLevel = 19;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 19)
            {
                ValuesScript.cellsToSpawn = 1750;
                ValuesScript.amountOfCellsUpgradeLevel = 20;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 20)
            {
                ValuesScript.cellsToSpawn = 1900;
                ValuesScript.amountOfCellsUpgradeLevel = 21;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 21)
            {
                ValuesScript.cellsToSpawn = 2050;
                ValuesScript.amountOfCellsUpgradeLevel = 22;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 22)
            {
                ValuesScript.cellsToSpawn = 2200;
                ValuesScript.amountOfCellsUpgradeLevel = 23;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 23)
            {
                ValuesScript.cellsToSpawn = 2350;
                ValuesScript.amountOfCellsUpgradeLevel = 24;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 24)
            {
                ValuesScript.cellsToSpawn = 2500;
                ValuesScript.amountOfCellsUpgradeLevel = 25;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 25)
            {
                ValuesScript.cellsToSpawn = 2650;
                ValuesScript.amountOfCellsUpgradeLevel = 26;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 26)
            {
                ValuesScript.cellsToSpawn = 2800;
                ValuesScript.amountOfCellsUpgradeLevel = 27;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 27)
            {
                ValuesScript.cellsToSpawn = 2950;
                ValuesScript.amountOfCellsUpgradeLevel = 28;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 28)
            {
                ValuesScript.cellsToSpawn = 3100;
                ValuesScript.amountOfCellsUpgradeLevel = 29;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 29)
            {
                ValuesScript.cellsToSpawn = 3500;
                ValuesScript.amountOfCellsUpgradeLevel = 30;
                ValuesScript.coinScore = ValuesScript.coinScore - UpgradeTwoPrice;
            }

        }
        else if (ValuesScript.coinScore < UpgradeTwoPrice)
        {
            print("Not enough points");
        }
    }

    /*
    public void removeMoreCellsUpgrade()
    {
        if (ValuesScript.amountOfCellsUpgradeLevel != 0)
        {
            if (ValuesScript.amountOfCellsUpgradeLevel == 1)
            {
                ValuesScript.cellsToSpawn = 10;
                ValuesScript.amountOfCellsUpgradeLevel = 0;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeTwoPrice - 60;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 2)
            {
                ValuesScript.cellsToSpawn = 15;
                ValuesScript.amountOfCellsUpgradeLevel = 1;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeTwoPrice - 60;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 3)
            {
                ValuesScript.cellsToSpawn = 20;
                ValuesScript.amountOfCellsUpgradeLevel = 2;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeTwoPrice - 60;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 4)
            {
                ValuesScript.cellsToSpawn = 25;
                ValuesScript.amountOfCellsUpgradeLevel = 3;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeTwoPrice - 60;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 5)
            {
                ValuesScript.cellsToSpawn = 30;
                ValuesScript.amountOfCellsUpgradeLevel = 4;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeTwoPrice - 60;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 6)
            {
                ValuesScript.cellsToSpawn = 35;
                ValuesScript.amountOfCellsUpgradeLevel = 5;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeTwoPrice - 60;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 7)
            {
                ValuesScript.cellsToSpawn = 40;
                ValuesScript.amountOfCellsUpgradeLevel = 6;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeTwoPrice - 60;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 8)
            {
                ValuesScript.cellsToSpawn = 45;
                ValuesScript.amountOfCellsUpgradeLevel = 7;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeTwoPrice - 60;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 9)
            {
                ValuesScript.cellsToSpawn = 50;
                ValuesScript.amountOfCellsUpgradeLevel = 8;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeTwoPrice - 60;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 10)
            {
                ValuesScript.cellsToSpawn = 55;
                ValuesScript.amountOfCellsUpgradeLevel = 9;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeTwoPrice - 60;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 11)
            {
                ValuesScript.cellsToSpawn = 60;
                ValuesScript.amountOfCellsUpgradeLevel = 10;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeTwoPrice - 60;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 12)
            {
                ValuesScript.cellsToSpawn = 70;
                ValuesScript.amountOfCellsUpgradeLevel = 11;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeTwoPrice - 60;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 13)
            {
                ValuesScript.cellsToSpawn = 80;
                ValuesScript.amountOfCellsUpgradeLevel = 12;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeTwoPrice - 60;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 14)
            {
                ValuesScript.cellsToSpawn = 90;
                ValuesScript.amountOfCellsUpgradeLevel = 13;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeTwoPrice - 60;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 15)
            {
                ValuesScript.cellsToSpawn = 100;
                ValuesScript.amountOfCellsUpgradeLevel = 14;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeTwoPrice - 60;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 16)
            {
                ValuesScript.cellsToSpawn = 110;
                ValuesScript.amountOfCellsUpgradeLevel = 15;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeTwoPrice - 60;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 17)
            {
                ValuesScript.cellsToSpawn = 120;
                ValuesScript.amountOfCellsUpgradeLevel = 16;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeTwoPrice - 60;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 18)
            {
                ValuesScript.cellsToSpawn = 130;
                ValuesScript.amountOfCellsUpgradeLevel = 17;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeTwoPrice - 60;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 19)
            {
                ValuesScript.cellsToSpawn = 140;
                ValuesScript.amountOfCellsUpgradeLevel = 18;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeTwoPrice - 60;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 20)
            {
                ValuesScript.cellsToSpawn = 150;
                ValuesScript.amountOfCellsUpgradeLevel = 19;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeTwoPrice - 60;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 21)
            {
                ValuesScript.cellsToSpawn = 160;
                ValuesScript.amountOfCellsUpgradeLevel = 20;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeTwoPrice - 60;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 22)
            {
                ValuesScript.cellsToSpawn = 190;
                ValuesScript.amountOfCellsUpgradeLevel = 21;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeTwoPrice - 60;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 23)
            {
                ValuesScript.cellsToSpawn = 210;
                ValuesScript.amountOfCellsUpgradeLevel = 22;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeTwoPrice - 60;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 24)
            {
                ValuesScript.cellsToSpawn = 240;
                ValuesScript.amountOfCellsUpgradeLevel = 23;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeTwoPrice - 60;
            }
            else if (ValuesScript.amountOfCellsUpgradeLevel == 25)
            {
                ValuesScript.cellsToSpawn = 270;
                ValuesScript.amountOfCellsUpgradeLevel = 24;
                ValuesScript.coinScore = ValuesScript.coinScore + UpgradeTwoPrice - 60;
            }
        }
        else if (ValuesScript.coinScore < 40)
        {
            print("Not enough points");
        }
    }
    */

    // THIRD UPGRADE


}
