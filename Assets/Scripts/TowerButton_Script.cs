using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButton_Script : MonoBehaviour
{
    public GameObject TowerBlueprint;

    public void SpawnTowerBluePrint()
    {
        Instantiate(TowerBlueprint);
    }
}
