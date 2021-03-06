using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    //This script was originally from this tutorial: https://youtu.be/Fj2JqISRR4s
    //Shooting elements were added onto it afterwards, using script from this video: https://youtu.be/wZ2UUOC17AY

    //Shooting Config
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public Transform MuzzlePosition;


    public enum TargetingType { First, Strong, Weak, Last }

    public TargetingType TargetingSystemToUse = TargetingType.First;

    GlobalData WorldAccessData;

    public LayerMask EnemyMask;
    public float Range = 25f;
    public float sphereVisionBubbleSize = 25;
    public GameObject Target;

    public float AttackSpeed = 1f;
    public float Damage = 5f;
    float AttackDelay;

    public float ProjectileForwardSpeed = 256f;
    public float ProjectileUpwardSpeed = 0f;

    public GameObject muzzleFlash;
    public Transform MuzzleFlashPosition;
    public ParticleSystem flamethrowerParticleEffect;
    public bool isFlamethrower = false;

    public float flamerParticleTimer;
    public bool isShootingFire = false;
    public GameObject visionSphere;


    private void Start()
    {

        WorldAccessData = FindObjectOfType<GlobalData>();
        AttackDelay = AttackSpeed;
    }

    private void Update()
    {
        visionSphere.gameObject.transform.localScale = new Vector3(sphereVisionBubbleSize, sphereVisionBubbleSize, sphereVisionBubbleSize);
        sphereVisionBubbleSize = (Range * 2f);
        if (flamerParticleTimer > 0)
        {
            flamerParticleTimer -= Time.deltaTime;
            isShootingFire = true;
            flamethrowerParticleEffect.Play();
        }
        if (flamerParticleTimer <= 0)
        {
            isShootingFire = false;
            flamethrowerParticleEffect.Stop();
        }


        if (Target)
        {
            Vector3 LookAtRot = new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z);

            transform.LookAt(LookAtRot);
            Attack();
        }
        else
        {
            if (EnemyInRange())
            {
                LookForEnemies();
            }
        }
    }

    private void Attack()
    {
        if(!alreadyAttacked)
        {
            if(isFlamethrower == false)
            {
                //New Shooting

                if (muzzleFlash != null)
                {
                    GameObject muzzleFlashInstantiated = Instantiate(muzzleFlash, MuzzleFlashPosition.position, MuzzleFlashPosition.rotation);
                    muzzleFlashInstantiated.SetActive(true);
                }

                //Make Bullet Appear
                GameObject projectileInstantiated = Instantiate(projectile, MuzzlePosition.position, MuzzlePosition.rotation);
                projectileInstantiated.SetActive(true);
                Rigidbody rb = projectileInstantiated.GetComponent<Rigidbody>();

                rb.AddForce(transform.forward * ProjectileForwardSpeed, ForceMode.Impulse);
                rb.AddForce(transform.up * ProjectileUpwardSpeed, ForceMode.Impulse);

                //Destroy(muzzleFlash);

                //Set alreadyAttacked to be false, set attack delay and continue to look for enemies
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
                LookForEnemies();


                //Old Shooting
                //Target.GetComponent<TowerDefenceAITest_V1>().TakeDamage(Damage);
                //LookForEnemies();
                //AttackDelay = AttackSpeed;
            }
            else if(isFlamethrower == true)
            {
                flamerParticleTimer = 0.2f;
                GameObject projectileInstantiated = Instantiate(projectile, MuzzlePosition.position, MuzzlePosition.rotation);
                projectileInstantiated.SetActive(true);
                Rigidbody rb = projectileInstantiated.GetComponent<Rigidbody>();

                rb.AddForce(transform.forward * ProjectileForwardSpeed, ForceMode.Impulse);
                rb.AddForce(transform.up * ProjectileUpwardSpeed, ForceMode.Impulse);

                //Destroy(muzzleFlash);

                //Set alreadyAttacked to be false, set attack delay and continue to look for enemies (and disable flame particle)
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
                LookForEnemies();
            }
        }
        else if(alreadyAttacked)
        {

            LookForEnemies();

            //Old Code
            //AttackDelay -= Time.deltaTime;
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    #region TargetingSystems

    private void LookForEnemies()
    {
        switch (TargetingSystemToUse)
        {
            case TargetingType.First:

                First();

                break;

            case TargetingType.Strong:

                Strong();

                break;

            case TargetingType.Weak:

                Weak();

                break;

            case TargetingType.Last:

                Last();

                break;

        }
    }

    private void Strong()
    {
        float HighestHP = Mathf.NegativeInfinity;
        GameObject StrongestEnemy = null;
        // Strong Targeting

        foreach (GameObject Enemy in WorldAccessData.EnemiesInScene)
        {
            if (Enemy)
            {
                if (Vector3.Distance(transform.position, Enemy.transform.position) <= Range)
                {
                    TowerDefenceAITest_V1 EnemySC = Enemy.GetComponent<TowerDefenceAITest_V1>();

                    if (EnemySC.health > HighestHP)
                    {
                        HighestHP = EnemySC.health;
                        StrongestEnemy = Enemy;
                    }
                }
            }
            else
            {
                continue;
            }
        }

        if (StrongestEnemy && Vector3.Distance(transform.position, StrongestEnemy.transform.position) <= Range)
        {
            Target = StrongestEnemy;
        }
    }

    private void First()
    {

        float ClosestDistance = Mathf.Infinity;
        GameObject ClosestEnemy = null;
        // First Targetting

        foreach (GameObject Enemy in WorldAccessData.EnemiesInScene)
        {
            if (Enemy)
            {
                if (Vector3.Distance(transform.position, Enemy.transform.position) <= Range)
                {
                    TowerDefenceAITest_V1 EnemySC = Enemy.GetComponent<TowerDefenceAITest_V1>();

                    if (EnemySC.TrueDistance < ClosestDistance)
                    {
                        ClosestDistance = EnemySC.TrueDistance;
                        ClosestEnemy = Enemy;
                    }
                }
            }
            else
            {
                continue;
            }
        }

        if (ClosestEnemy != null && Vector3.Distance(transform.position, ClosestEnemy.transform.position) <= Range)
        {
            Target = ClosestEnemy;
        }
        else
        {
            Target = null;
        }
    }

    private void Weak()
    {
        float LowestHP = Mathf.Infinity;
        GameObject StrongestEnemy = null;
        // Strong Targeting

        foreach (GameObject Enemy in WorldAccessData.EnemiesInScene)
        {
            if (Enemy)
            {
                if (Vector3.Distance(transform.position, Enemy.transform.position) <= Range)
                {
                    TowerDefenceAITest_V1 EnemySC = Enemy.GetComponent<TowerDefenceAITest_V1>();

                    if (EnemySC.health < LowestHP)
                    {
                        LowestHP = EnemySC.health;
                        StrongestEnemy = Enemy;
                    }
                }
            }
            else
            {
                continue;
            }
        }

        if (StrongestEnemy && Vector3.Distance(transform.position, StrongestEnemy.transform.position) <= Range)
        {
            Target = StrongestEnemy;
        }
    }

    private void Last()
    {
        float ClosestDistance = Mathf.NegativeInfinity;
        GameObject ClosestEnemy = null;
        // First Targetting

        foreach (GameObject Enemy in WorldAccessData.EnemiesInScene)
        {
            if (Enemy)
            {
                if (Vector3.Distance(transform.position, Enemy.transform.position) <= Range)
                {
                    TowerDefenceAITest_V1 EnemySC = Enemy.GetComponent<TowerDefenceAITest_V1>();

                    if (EnemySC.TrueDistance > ClosestDistance)
                    {
                        ClosestDistance = EnemySC.TrueDistance;
                        ClosestEnemy = Enemy;
                    }
                }
            }
            else
            {
                continue;
            }
        }

        if (ClosestEnemy && Vector3.Distance(transform.position, ClosestEnemy.transform.position) <= Range)
        {
            Target = ClosestEnemy;
        }
    }

    private bool EnemyInRange()
    {
        return Physics.CheckSphere(transform.position, Range, EnemyMask);
    }

    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }

}
