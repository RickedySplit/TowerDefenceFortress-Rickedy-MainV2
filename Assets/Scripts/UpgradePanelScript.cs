using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelScript : MonoBehaviour
{
    private TurretScript TurretScript;
    public GameObject coolSelf;

    public void SetUpgradeTarget(GameObject self)
    {
        coolSelf = self;
        Debug.Log("Gamering");
        TurretScript = coolSelf.GetComponent<TurretScript>();
    }

    public void UpgradeFireRate()
    {
        {
            if(TurretScript.timeBetweenAttacks > 0.15f)
            {
                TurretScript.timeBetweenAttacks -= 0.1f;
                Debug.Log("Fire Rate has been upgraded");
            }
            else if(TurretScript.timeBetweenAttacks <= 0.15f)
            {
                Debug.Log("Can't upgrade Fire Rate Further");
            }
        }
    }
    public void UpgradeRange()
    {
        {
            if(TurretScript.Range < 20)
            {
                TurretScript.Range += 1;
                Debug.Log("Range has been upgraded");
            }
            else if(TurretScript.Range >= 20)
            {
                Debug.Log("Can't upgrade Range Further");
            }
        }
    }

}
