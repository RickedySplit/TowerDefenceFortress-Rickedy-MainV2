using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{

    public Transform[] PathNodes;

    public GameObject[] EnemiesInScene;
    public Transform StartPoint;

    private void Update()
    {
        UpdateArrays();
    }

    public void UpdateArrays()
    {
        EnemiesInScene = GameObject.FindGameObjectsWithTag("EnemyTag");
    }

}
