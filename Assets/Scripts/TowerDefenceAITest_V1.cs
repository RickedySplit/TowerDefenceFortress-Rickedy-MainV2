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
    private float moveSpeed = 2f;

    // Index of current waypoint from which Enemy walks
    // to the next one
    private int waypointIndex = 0;

    int IndexToTravelTo = 0;
    GlobalData GameData;
    public float health = 10f;
    public float DistanceToNode;
    public float TrueDistance = 0f;
    public TextMeshProUGUI healthText;


    // Use this for initialization
    private void Start()
    {
        GameData = FindObjectOfType<GlobalData>();
        // Set position of Enemy as position of the first waypoint
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
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

    // Method that actually make Enemy walk
    private void Move()
    {
        // If Enemy didn't reach last waypoint it can move
        // If enemy reached last waypoint then it stops
        if (waypointIndex <= waypoints.Length - 1)
        {
            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector2.MoveTowards(transform.position,
               waypoints[waypointIndex].transform.position,
               moveSpeed * Time.deltaTime);

            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
