using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtonScript_V1 : MonoBehaviour
{
    private TurretScript TurretScript;
    private TowerProjectileBase TowerProjectileBase;
    public GameObject upgradePanel;
    public GameObject playerEmptyObj;
    public GameObject self;
    public GameObject UsedProjectile;
    public bool isCurrentlySelected = false;
    public ParticleSystem selectionParticle;
    public GameObject visionSphere;

    void Awake()
    {
        playerEmptyObj = GameObject.Find("PlayerEmptyObject");
        //upgradePanel = GameObject.Find("UpgradeMenu");
        //TurretScript = gameObject.GetComponent<TurretScript>();
        //TowerProjectileBase = gameObject.GetComponent<TowerProjectileBase>();

    }

    void Update()
    {
        if (playerEmptyObj.GetComponent<PlayerResourcesScript>().currentlySelectedTower == self)
        {
            isCurrentlySelected = true;
            IsSelectedMethod();
            selectionParticle.Play();
            upgradePanel.SetActive(true);
            visionSphere.SetActive(true);
        }
        else if (playerEmptyObj.GetComponent<PlayerResourcesScript>().currentlySelectedTower != self)
        {
            isCurrentlySelected = false;
            IsNotSelectedMethod();
            selectionParticle.Stop();
            upgradePanel.SetActive(false);
            visionSphere.SetActive(false);
        }
    }

    void IsSelectedMethod()
    {
        //selectionParticle.Play();
        //upgradePanel.SetActive(true);
    }

    void IsNotSelectedMethod()
    {
        //selectionParticle.Stop();
        //upgradePanel.SetActive(false);
    }

    //public void selectSelf()
    //{
    //    upgradePanel.GetComponent<UpgradePanelScript>().SetUpgradeTarget(self, UsedProjectile);
    //}

    public void OnMouseUpAsButton()
    {
        playerEmptyObj.GetComponent<PlayerResourcesScript>().SetUpgradeTarget(self);
        //upgradePanel.GetComponent<UpgradePanelScript>().SetUpgradeTarget(self, UsedProjectile);

    }

}
