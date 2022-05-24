using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelScript : MonoBehaviour
{
    private TurretScript TurretScript;
    private TowerProjectileBase TowerProjectileBase;
    public GameObject coolSelf;
    public GameObject coolUsedProjectile;

    //public void SetUpgradeTarget(GameObject self, GameObject UsedProjectile)
    //{
    //    coolUsedProjectile = UsedProjectile;
    //    coolSelf = self;
    //    Debug.Log("Gamering");
    //    TowerProjectileBase = coolUsedProjectile.GetComponent<TowerProjectileBase>();
    //    TurretScript = coolSelf.GetComponent<TurretScript>();
    //}

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
            if(TurretScript.Range < 30f)
            {
                TurretScript.Range += 1f;
                Debug.Log("Range has been upgraded");
            }
            else if(TurretScript.Range >= 30f)
            {
                Debug.Log("Can't upgrade Range Further");
            }
        }
    }

    public void UpgradeDamage()
    {
        {
            if(TowerProjectileBase.damage < 32f)
            {
                TowerProjectileBase.damage += 1;
                TowerProjectileBase.explosionDamage += 0.5f;
                Debug.Log("Damage has been upgraded");
            }
            else if(TowerProjectileBase.damage >= 32f)
            {
                Debug.Log("Can't upgrade Damage Further");
            }
        }
    }

}
