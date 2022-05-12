using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtonScript_V1 : MonoBehaviour
{
    private TurretScript TurretScript;
    public GameObject upgradePanel;
    public GameObject self;

    void Awake()
    {
        TurretScript = gameObject.GetComponent<TurretScript>();
    }

    public void selectSelf()
    {
        upgradePanel.GetComponent<UpgradePanelScript>().SetUpgradeTarget(self);

    }
}
