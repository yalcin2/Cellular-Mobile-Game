using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillShopScript : MonoBehaviour
{
    public GameObject skill_interface;

    public Text currentPoints;

    public GameObject showFirstTierUpgrade;
    public GameObject showFirstTierUpgrade_2;

    public Text currentFirstSubSkill;
    public Text currentFirstSubSkill_2;
    public Button addFirstSubSkillBtn;
    public Button addFirstSubSkillBtn_2;

    public Button unlockFirstSkillButton;
    public Button unlockSecondSkillButton;

    // Start is called before the first frame update
    void Start()
    {
        skill_interface.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        currentPoints.text = ValuesScript.levelPoints.ToString();

        if (ValuesScript.magnetPower == 5)
        {
            currentFirstSubSkill.text = "MAX";
        }
        else if (ValuesScript.magnetPower < 5)
        {
            currentFirstSubSkill.text = ValuesScript.magnetPower.ToString();
        }

        if (ValuesScript.magnetRange == 3)
        {
            currentFirstSubSkill_2.text = "MAX";
        }
        else if (ValuesScript.magnetRange < 3)
        {
            currentFirstSubSkill_2.text = ValuesScript.magnetRange.ToString();
        }

        if (ValuesScript.fastestCellSpawnUnlocked == "yes")
        {
            unlockSecondSkillButton.interactable = false;
        }
        else if (ValuesScript.fastestCellSpawnUnlocked == "no")
        {
            unlockSecondSkillButton.interactable = true;
        }
        if (ValuesScript.magnetUnlocked == "yes")
        {
            showFirstTierUpgrade.SetActive(false);
            showFirstTierUpgrade_2.SetActive(false);
            unlockFirstSkillButton.interactable = false;
        }
        else if (ValuesScript.magnetUnlocked == "no")
        {
            showFirstTierUpgrade.SetActive(true);
            showFirstTierUpgrade_2.SetActive(true);
            unlockFirstSkillButton.interactable = true;
        }
        if (ValuesScript.magnetPower == 5) {
            addFirstSubSkillBtn.interactable = false;
        }
        else if (ValuesScript.magnetPower < 5) {
            addFirstSubSkillBtn.interactable = true;
        }
        if (ValuesScript.magnetRange == 3)
        {
            addFirstSubSkillBtn_2.interactable = false;
        }
        else if (ValuesScript.magnetRange < 3)
        {
            addFirstSubSkillBtn_2.interactable = true;
        }
    }

    public void onSkillBtnPressed()
    {
        skill_interface.SetActive(true);
        Time.timeScale = 0f;
    }

    public void onBackBtnPressed()
    {
        skill_interface.SetActive(false);
        Time.timeScale = 1f;
    }

    ////// FIRST SKILL
    
    public void UnlockMagnetSkill() {
        if (ValuesScript.levelPoints >= 3 && ValuesScript.magnetUnlocked == "no") {
            ValuesScript.levelPoints = ValuesScript.levelPoints - 3;
            ValuesScript.magnetUnlocked = "yes";
        }
        else if (ValuesScript.levelPoints < 3) {
            print("NOT ENOUGH POINTS");
        }
    }

    // 1 SUB: FIRST SKILL
    public void IncreaseMagnetPower()
    {
        if (ValuesScript.levelPoints >= 1)
        {
            if (ValuesScript.magnetPower == 1)
            {
                ValuesScript.magnetPower = 2;
                ValuesScript.levelPoints = ValuesScript.levelPoints - 1;
            }
            else if (ValuesScript.magnetPower == 2)
            {
                ValuesScript.magnetPower = 3;
                ValuesScript.levelPoints = ValuesScript.levelPoints - 1;
            }
            else if (ValuesScript.magnetPower == 3)
            {
                ValuesScript.magnetPower = 4;
                ValuesScript.levelPoints = ValuesScript.levelPoints - 1;
            }
            else if (ValuesScript.magnetPower == 4)
            {
                ValuesScript.magnetPower = 5;
                ValuesScript.levelPoints = ValuesScript.levelPoints - 1;
            }
        }
    }

    // 2 SUB: FIRST SKILL

    public void IncreaseMagnetRange()
    {
        if (ValuesScript.levelPoints >= 1)
        {
            if (ValuesScript.magnetRange == 1)
            {
                LevelScript.magnetRangeIncreased = true;
                ValuesScript.magnetRange = 2;
                ValuesScript.levelPoints = ValuesScript.levelPoints - 2;
            }
            else if (ValuesScript.magnetRange == 2)
            {
                LevelScript.magnetRangeIncreased = true;
                ValuesScript.magnetRange = 3;
                ValuesScript.levelPoints = ValuesScript.levelPoints - 2;
            }
        }
    }

    ////// SECOND SKILL

    public void UnlockInsaneSkill()
    {
        if (ValuesScript.levelPoints >= 4 &&
            ValuesScript.fastestCellSpawnUnlocked == "no" && 
            ValuesScript.cellsSpawnUpgradeLevel == 10)
        {
            ValuesScript.levelPoints = ValuesScript.levelPoints - 4;
            ValuesScript.fastestCellSpawnUnlocked = "yes";
        }
        else if (ValuesScript.levelPoints < 4)
        {
            print("NOT ENOUGH POINTS");
        }
    }


}
