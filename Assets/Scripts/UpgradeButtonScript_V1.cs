using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtonScript_V1 : MonoBehaviour
{
    private TurretScript TurretScript;

    void Awake()
    {
        TurretScript = gameObject.GetComponent<TurretScript>();
    }

    public void UpgradeFireRate()
    {
        {
            if(TurretScript.timeBetweenAttacks > 0.15f)
            {
                TurretScript.timeBetweenAttacks -= 0.1f;
            }
            else if(TurretScript.timeBetweenAttacks <= 0.15f)
            {
                Debug.Log("Can't upgrade Fire Rate Further");
            }
        }
    }
}
