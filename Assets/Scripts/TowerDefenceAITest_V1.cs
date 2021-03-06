using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class TowerDefenceAITest_V1 : MonoBehaviour
{
    //This script is taken from this tutorial: https://youtu.be/KoFDDp5W5p0

    // Array of waypoints to walk from one to the next one
    [SerializeField]
    private Transform[] waypoints;

    // Walk speed that can be set in Inspector
    [SerializeField]
    public float MoveSpeed = 2f;
    public float currentMoveSpeed = 2f;
    public float slowedMoveSpeed;

    // Index of current waypoint from which Enemy walks
    // to the next one
    private int waypointIndex = 0;

    int IndexToTravelTo = 0;
    GlobalData GameData;
    public float maxHealth = 10f;
    public float health = 10f;
    public float DistanceToNode;
    public float TrueDistance = 0f;
    public Slider healthBarSlider;
    public TextMeshProUGUI healthText;


    public bool jarated = false;
    public ParticleSystem JarateDropletParticles;
    public float JarateTimer;

    public bool BulletSlowed = false;
    public ParticleSystem BulletSlowdownParticles;
    public float BulletSlowdownTimer;

    public bool onFire = false;
    public ParticleSystem AfterburnParticles;
    public float AfterburnTimer;


    public GameObject playerEmptyObject;
    public int moneyRewardOnDeath;



    // Use this for initialization
    private void Awake()
    {
        playerEmptyObject = GameObject.Find("PlayerEmptyObject");
    }
    private void Start()
    {
        health = maxHealth;
        slowedMoveSpeed = MoveSpeed * 0.66f;
        GameData = FindObjectOfType<GlobalData>();
        // Set position of Enemy as position of the first waypoint
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = health;

        if (JarateTimer > 0)
        {
            JarateTimer -= Time.deltaTime;
        }

        if (jarated == false)
        {
            JarateDropletParticles.Stop();
        }

        if (JarateTimer == 0)
        {
            jarated = false;
        }


        if (BulletSlowdownTimer > 0)
        {
            BulletSlowdownTimer -= Time.deltaTime;
        }

        if (BulletSlowed == false)
        {
            currentMoveSpeed = MoveSpeed;
            BulletSlowdownParticles.Stop();
        }

        if (BulletSlowdownTimer == 0)
        {
            currentMoveSpeed = MoveSpeed;
            BulletSlowed = false;
        }


        if (AfterburnTimer > 0)
        {
            AfterburnTimer -= Time.deltaTime;
        }

        if (onFire == false)
        {
            AfterburnParticles.Stop();
            StopCoroutine(burnEverySecond());
        }

        if (JarateTimer == 0)
        {
            onFire = false;
        }



        healthText.text = health.ToString();

        // Move Enemy
        Move();

        
        TrueDistance = 0f;
        TrueDistance += DistanceToNode;
        for (int i = IndexToTravelTo; i < GameData.PathNodes.Length; i++)
        {
            if(i < 4)
            {
                TrueDistance += GameData.PathNodes[i].GetComponent<DistanceToNode>().Distance;
            }
            else
            {
                break;
            }

        }
    }

    public void CoverInJarate()
    {
        if (jarated == true)
        {
            JarateTimer = 3.5f;
        }
        else if (jarated == false)
        {
            JarateDropletParticles.Play();
            jarated = true;
            JarateTimer = 3.5f;
        }
    }

    public void SlowDownViaBulletSlowdown()
    {
        if (BulletSlowed == true)
        {
            currentMoveSpeed = slowedMoveSpeed;
            BulletSlowdownTimer = 2.5f;
        }
        else if (BulletSlowed == false)
        {
            currentMoveSpeed = slowedMoveSpeed;
            BulletSlowdownParticles.Play();
            BulletSlowed = true;
            BulletSlowdownTimer = 2.5f;
        }
    }

    public void ApplyAfterburn()
    {
        if (onFire == true)
        {
            AfterburnTimer = 4f;
        }
        else if (onFire == false)
        {
            AfterburnParticles.Play();
            StartCoroutine(burnEverySecond());
            onFire = true;
            AfterburnTimer = 4f;
        }
    }

    IEnumerator burnEverySecond() 
    {
        while (true) 
        {
            TakeDamage(.35f);
            yield return new WaitForSeconds(1f);
        }
    }


    // Method that actually make Enemy walk
    private void Move()
    {
        // If Enemy didn't reach last waypoint it can move
        // If enemy reached last waypoint then it stops
        if (waypointIndex <= waypoints.Length - 1)
        {
            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector3.MoveTowards(transform.position,
               waypoints[waypointIndex].transform.position,
               currentMoveSpeed * Time.deltaTime);

            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
        }
    }
    public void TakeDamage(float damage)
    {
        if (jarated == true)
        {
            health -= (damage * 1.5f);

            if(health <= 0)
            {
                playerEmptyObject.GetComponent<PlayerResourcesScript>().robotsKilled =+ 1;
                playerEmptyObject.GetComponent<PlayerResourcesScript>().GivePlayerMoney(moneyRewardOnDeath);
                Destroy(gameObject);
            }
        }
        else if (jarated == false)
        {
            health -= damage;

            if(health <= 0)
            {
                playerEmptyObject.GetComponent<PlayerResourcesScript>().robotsKilled++;
                playerEmptyObject.GetComponent<PlayerResourcesScript>().GivePlayerMoney(moneyRewardOnDeath);
                Destroy(gameObject);
            }
        }   
    }
}
