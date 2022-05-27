using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerResourcesScript : MonoBehaviour
{
    public int playerMoney;
    public TMP_Text playerMoneyText;
    public int playerLives;
    public TMP_Text playerLivesText;
    public GameObject currentlySelectedTower;
    public int robotsKilled = 0;
    public TMP_Text robotsKilledText;

    public GameObject ScoutTowerBlueprint;
    public GameObject SoldierTowerBlueprint;
    public GameObject PyroTowerBlueprint;
    public GameObject HeavyTowerBlueprint;
    public GameObject SniperTowerBlueprint;


    public void SetUpgradeTarget(GameObject self)
    {
        currentlySelectedTower = self;
        Debug.Log(currentlySelectedTower +" Selected");
    }




    //Note, the emblembs used for the buttons (Which these Methods are used for, are found here: https://wiki.teamfortress.com/wiki/Category:Class_emblems)

    public void buyScoutTower()
    {
        if (playerMoney >= 200)
        {
            playerMoney -= 200;
            Instantiate(ScoutTowerBlueprint);
        }
        else
        {
            Debug.Log("You're too poor to afford a Scout Tower, Chucklenuts!");
        }
    }

    public void buySoldierTower()
    {
        if (playerMoney >= 425)
        {
            playerMoney -= 425;
            Instantiate(SoldierTowerBlueprint);
        }
        else
        {
            Debug.Log("You're too poor to afford a Soldier Tower, Maggot!");
        }
    }

    public void buyPyroTower()
    {
        if (playerMoney >= 350)
        {
            playerMoney -= 350;
            Instantiate(PyroTowerBlueprint);
        }
        else
        {
            Debug.Log("You're too poor to afford a Pyro Tower, Mmmph!");
        }
    }

        public void buyHeavyTower()
    {
        if (playerMoney >= 750)
        {
            playerMoney -= 750;
            Instantiate(HeavyTowerBlueprint);
        }
        else
        {
            Debug.Log("You're too poor to afford a Heavy Tower, Baby Man!");
        }
    }

    public void buySniperTower()
    {
        if (playerMoney >= 950)
        {
            playerMoney -= 950;
            Instantiate(SniperTowerBlueprint);
        }
        else
        {
            Debug.Log("You're too poor to afford a Sniper Tower, P1ss Off!");
        }
    }


    public void GivePlayerMoney(int moneyRewardOnDeath)
    {
        playerMoney += moneyRewardOnDeath;
    }


    public void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        if(playerLives <= 0)
        {
            SceneManager.LoadScene("3DSceneTest_V1");
        }


        robotsKilledText.text = "Robots Killed: " + robotsKilled.ToString();
        playerMoneyText.text = "Current Money: $" + playerMoney.ToString();
        playerLivesText.text = playerLives.ToString() + " Lives";
    }
}
