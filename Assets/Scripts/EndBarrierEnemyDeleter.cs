using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBarrierEnemyDeleter : MonoBehaviour
{
    public PlayerResourcesScript PlayerResources;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyTag"))
        {
            PlayerResources.playerLives -= 1;
            Destroy(other.gameObject);
        }
    }
}
