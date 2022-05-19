using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtonScript_V1 : MonoBehaviour
{
    private TurretScript TurretScript;
    private TowerProjectileBase TowerProjectileBase;
    public GameObject upgradePanel;
    public GameObject self;
    public GameObject UsedProjectile;

    void Awake()
    {
        TurretScript = gameObject.GetComponent<TurretScript>();
        TowerProjectileBase = gameObject.GetComponent<TowerProjectileBase>();
    }

    //public void selectSelf()
    //{
    //    upgradePanel.GetComponent<UpgradePanelScript>().SetUpgradeTarget(self, UsedProjectile);
    //}

    public void OnMouseUpAsButton()
    {
        upgradePanel.GetComponent<UpgradePanelScript>().SetUpgradeTarget(self, UsedProjectile);

    }

}
