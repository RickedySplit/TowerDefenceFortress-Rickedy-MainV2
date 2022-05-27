using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelScript : MonoBehaviour
{
    private TurretScript TurretScript;
    private TowerProjectileBase TowerProjectileBase;
    public GameObject coolSelf;
    public GameObject coolUsedProjectile;
    private PlayerResourcesScript PlayerResourcesScript;
    public GameObject playerResourcesObj;

    public GameObject normalCosmeticHead;
    public GameObject upgrade1CosmeticHead;
    public GameObject upgrade2CosmeticHead;

    public int fireRateUpgradeCost = 9;
    public int currentFireRateUpgradeAmount = 0;
    public int maxFireRateUpgradeAmount = 3;

    public int rangeUpgradeCost = 4;
    public int currentRangeUpgradeAmount = 0;
    public int maxRangeUpgradeAmount = 3;

    public int damageUpgradeCost = 20;
    public int currentDamageUpgradeAmount = 0;
    public int maxDamageUpgradeAmount = 3;

    public bool alreadyHasWeaponUpgrade = false;

    public int directHitUpgradeCost = 120;
    //public bool hasDirectHitUpgrade = false;
    public bool canHaveDirectHit = false;

    public int sydneySleeperUpgradeCost = 120;
    //public bool hasSydneySleeperUpgrade = false;
    public bool canHaveSydneySleeper = false;

    public int forceANatureUpgradeCost = 120;
    public bool canHaveForceANature = false;

    public int nataschaUpgradeCost = 120;
    public bool canHaveNatascha = false;

    public int tomislavUpgradeCost = 120;
    public bool canHaveTomislav = false;


    public void Start()
    {
        playerResourcesObj = GameObject.Find("PlayerEmptyObject");
        PlayerResourcesScript = playerResourcesObj.GetComponent<PlayerResourcesScript>();
        TowerProjectileBase = coolUsedProjectile.GetComponent<TowerProjectileBase>();
        TurretScript = coolSelf.GetComponent<TurretScript>();
    }

    public void Awake()
    {
        playerResourcesObj = GameObject.Find("PlayerEmptyObject");
        PlayerResourcesScript = playerResourcesObj.GetComponent<PlayerResourcesScript>();
        TowerProjectileBase = coolUsedProjectile.GetComponent<TowerProjectileBase>();
        TurretScript = coolSelf.GetComponent<TurretScript>();
    }

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
            //if(TurretScript.timeBetweenAttacks > 0.15f)
            if(currentFireRateUpgradeAmount < maxFireRateUpgradeAmount)
            {
                PlayerResourcesScript.playerMoney -= fireRateUpgradeCost;
                TurretScript.timeBetweenAttacks -= 0.1f;
                Debug.Log("Fire Rate has been upgraded");
                fireRateUpgradeCost *= 2;
                currentFireRateUpgradeAmount += 1;
            }
            //else if(TurretScript.timeBetweenAttacks <= 0.15f)
            else if(currentFireRateUpgradeAmount >= maxFireRateUpgradeAmount)
            {
                Debug.Log("Can't upgrade Fire Rate Further");
            }
            else if(PlayerResourcesScript.playerMoney >= fireRateUpgradeCost)
            {
                Debug.Log("Can't afford Fire Rate Upgrade");
            }
        }
    }
    public void UpgradeRange()
    {
        {
            //if(TurretScript.Range < 24f)
            if(currentRangeUpgradeAmount < maxRangeUpgradeAmount)
            {
                PlayerResourcesScript.playerMoney -= rangeUpgradeCost;
                TurretScript.Range += 1.5f;
                Debug.Log("Range has been upgraded");
                rangeUpgradeCost *= 2;
                currentRangeUpgradeAmount += 1;
            }
            //else if(TurretScript.Range >= 24f)
            else if(currentRangeUpgradeAmount >= maxRangeUpgradeAmount)
            {
                Debug.Log("Can't upgrade Range Further");
            }
            else if(PlayerResourcesScript.playerMoney >= rangeUpgradeCost)
            {
                Debug.Log("Can't afford Range Upgrade");
            }
        }
    }

    public void UpgradeDamage()
    {
        {
            //if(TowerProjectileBase.damage < 32f)
            if(currentDamageUpgradeAmount < maxDamageUpgradeAmount)
            {
                PlayerResourcesScript.playerMoney -= damageUpgradeCost;
                TowerProjectileBase.damage += 1;
                TowerProjectileBase.explosionDamage += 0.25f;
                Debug.Log("Damage has been upgraded");
                damageUpgradeCost *= 2;
                currentDamageUpgradeAmount += 1;
            }
            //else if(TowerProjectileBase.damage >= 32f)
            else if(currentDamageUpgradeAmount >= maxDamageUpgradeAmount)
            {
                Debug.Log("Can't upgrade Damage Further");
            }
            else if(PlayerResourcesScript.playerMoney >= damageUpgradeCost)
            {
                Debug.Log("Can't afford Damage Upgrade");
            }
        }
    }

    public void DirectHitUpgrade()
    {
        {
            if((alreadyHasWeaponUpgrade == false) && (canHaveDirectHit == true))
            {
                PlayerResourcesScript.playerMoney -= directHitUpgradeCost;
                TowerProjectileBase.damage += 14;
                TowerProjectileBase.explosionDamage -= 1f;
                TowerProjectileBase.explosionRange -= 3f;
                TowerProjectileBase.maxLifetime += 2f;
                TurretScript.ProjectileForwardSpeed *= 2;

                Debug.Log("Direct Hit Acquired!");
                alreadyHasWeaponUpgrade = true;

                normalCosmeticHead.SetActive(false);
                upgrade1CosmeticHead.SetActive(true);
            }
            else if(alreadyHasWeaponUpgrade == true)
            {
                Debug.Log("You Already have a Weapon!");
            }
            else if(PlayerResourcesScript.playerMoney >= directHitUpgradeCost)
            {
                Debug.Log("Can't afford the Direct Hit!");
            }
            else if(canHaveDirectHit == false)
            {
                Debug.Log("Cannot give Direct Hit to this Tower Type!");
            }
        }
    }

    public void SydneySleeperUpgrade()
    {
        {
            if((alreadyHasWeaponUpgrade == false) && (canHaveSydneySleeper == true))
            {
                PlayerResourcesScript.playerMoney -= sydneySleeperUpgradeCost;
                TowerProjectileBase.damage = 1;
                TowerProjectileBase.explodeOnTouch = true;
                TowerProjectileBase.ApplyJarateOnHit = true;
                TowerProjectileBase.ApplyJarateOnExplosion = true;
                TurretScript.timeBetweenAttacks = 2.25f;
                TurretScript.Range *= 0.5f;

                Debug.Log("Sydney Sleeper Acquired!");
                alreadyHasWeaponUpgrade = true;

                normalCosmeticHead.SetActive(false);
                upgrade1CosmeticHead.SetActive(true);
            }
            else if(alreadyHasWeaponUpgrade == true)
            {
                Debug.Log("You Already have a Weapon!");
            }
            else if(PlayerResourcesScript.playerMoney >= sydneySleeperUpgradeCost)
            {
                Debug.Log("Can't afford the Sydney Sleeper!");
            }
            else if(canHaveSydneySleeper == false)
            {
                Debug.Log("Cannot give Sydney Sleeper to this Tower Type!");
            }
        }
    }

    public void ForceANatureUpgrade()
    {
        {
            if ((alreadyHasWeaponUpgrade == false) && (canHaveForceANature == true))
            {
                PlayerResourcesScript.playerMoney -= forceANatureUpgradeCost;
                TowerProjectileBase.damage *= 4.5f;
                TurretScript.Range *= 0.8f;
                TurretScript.timeBetweenAttacks += 1f;

                Debug.Log("Force-A-Nature Acquired!");
                alreadyHasWeaponUpgrade = true;

                normalCosmeticHead.SetActive(false);
                upgrade1CosmeticHead.SetActive(true);
            }
            else if (alreadyHasWeaponUpgrade == true)
            {
                Debug.Log("You Already have a Weapon!");
            }
            else if (PlayerResourcesScript.playerMoney >= forceANatureUpgradeCost)
            {
                Debug.Log("Can't afford the Force-A-Nature!");
            }
            else if (canHaveForceANature == false)
            {
                Debug.Log("Cannot give Force-A-Nature to this Tower Type!");
            }
        }
    }

    public void NataschaUpgrade()
    {
        {
            if ((alreadyHasWeaponUpgrade == false) && (canHaveNatascha == true))
            {
                PlayerResourcesScript.playerMoney -= nataschaUpgradeCost;
                TowerProjectileBase.damage *= 0.65f;
                TurretScript.Range *= 0.85f;
                TurretScript.timeBetweenAttacks *= 1.4f;
                TowerProjectileBase.ApplyBulletSlowdownOnHit = true;

                Debug.Log("Natascha Acquired!");
                alreadyHasWeaponUpgrade = true;

                normalCosmeticHead.SetActive(false);
                upgrade1CosmeticHead.SetActive(true);
            }
            else if (alreadyHasWeaponUpgrade == true)
            {
                Debug.Log("You Already have a Weapon!");
            }
            else if (PlayerResourcesScript.playerMoney >= nataschaUpgradeCost)
            {
                Debug.Log("Can't afford the Natascha!");
            }
            else if (canHaveNatascha == false)
            {
                Debug.Log("Cannot give Natascha to this Tower Type!");
            }
        }
    }

    public void TomislavUpgrade()
    {
        {
            if ((alreadyHasWeaponUpgrade == false) && (canHaveTomislav == true))
            {
                PlayerResourcesScript.playerMoney -= tomislavUpgradeCost;
                TurretScript.Range *= 1.75f;
                TurretScript.timeBetweenAttacks *= 2f;

                Debug.Log("Tomislav Acquired!");
                alreadyHasWeaponUpgrade = true;

                normalCosmeticHead.SetActive(false);
                upgrade2CosmeticHead.SetActive(true);
            }
            else if (alreadyHasWeaponUpgrade == true)
            {
                Debug.Log("You Already have a Weapon!");
            }
            else if (PlayerResourcesScript.playerMoney >= tomislavUpgradeCost)
            {
                Debug.Log("Can't afford the Tomislav!");
            }
            else if (canHaveTomislav == false)
            {
                Debug.Log("Cannot give Tomislav to this Tower Type!");
            }
        }
    }

}
