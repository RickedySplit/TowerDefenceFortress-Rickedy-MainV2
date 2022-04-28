using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerResourcesScript : MonoBehaviour
{
    public int playerMoney;
    public TMP_Text playerMoneyText;
    public int playerLives;
    public TMP_Text playerLivesText;

    //Note, the emblembs used for the buttons (Which these Methods are used for, are found here: https://wiki.teamfortress.com/wiki/Category:Class_emblems)

    public void buyScoutTower()
    {
        if (playerMoney >= 200)
        {
            playerMoney -= 200;
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
        }
        else
        {
            Debug.Log("You're too poor to afford a Pyro Tower, Mmmph!");
        }
    }


    public void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        playerMoneyText.text = "Current Money: $" + playerMoney.ToString();
        playerLivesText.text = playerLives.ToString() + " Lives";
    }
}
