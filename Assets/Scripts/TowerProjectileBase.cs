using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectileBase : MonoBehaviour
{
    //Assignables
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask EnemyLayer;

    //Stats
    public bool useGravity;

    //Damage
    public int explosionDamage;
    public float explosionRange;

    //Lifetime
    public int maxCollisions; //will die when reachning maximum amount of collisions with anything
    public float maxLifetime; //will die on lifetime expiring
    public bool explodeOnTouch; //will die on contact with enemy

    int collisions;
    PhysicMaterial physics_mat;


    private void Update()
    {
        //When to explode:
        if (collisions > maxCollisions) Explode();

        //Count down lifetime
        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0 ) Explode();
    }

    private void Explode()
    {
        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);

        //Check for enemies
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, EnemyLayer);
        for (int i = 0; i < enemies.Length; i++)
        {
           //Get component of enemy and call Take Damage
            enemies[i].GetComponent<TowerDefenceAITest_V1>().TakeDamage(explosionDamage);
        }

        //Add a small delay to projectiel destruction
        Invoke("DestroyDelay", 0.05f);
    }

    private void DestroyDelay()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Count up collisions
        collisions++;

        //Explode if bullet hits an enemy directly and explodeOnTouch is activated
        if (collision.collider.CompareTag("EnemyTag") && explodeOnTouch) Explode();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
    
}
