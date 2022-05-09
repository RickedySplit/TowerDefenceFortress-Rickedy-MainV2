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
    public int damage;
    public float explosionRange;

    //Lifetime
    public int maxCollisions; //will die when reachning maximum amount of collisions with anything
    public float maxLifetime; //will die on lifetime expiring
    public bool explodeOnTouch; //will die on contact with enemy
    public GameObject impactVFX;

    public bool ApplyJarateOnExplosion = false;
    public bool ApplyJarateOnHit = false;

    public bool ApplyBulletSlowdownOnExplosion = false;
    public bool ApplyBulletSlowdownOnHit = false;

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
                if (ApplyJarateOnExplosion == true)
                {
                    enemies[i].GetComponent<TowerDefenceAITest_V1>().CoverInJarate();
                }
                if (ApplyBulletSlowdownOnExplosion == true)
                {
                    enemies[i].GetComponent<TowerDefenceAITest_V1>().SlowDownViaBulletSlowdown();
                }
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

        GameObject other = collision.gameObject;
        if (collision.collider.CompareTag("EnemyTag") && !explodeOnTouch)
        {
            var impact = Instantiate (impactVFX, collision.contacts[0].point, Quaternion.identity) as GameObject;
            Destroy(impact, 2);
        }

        {
            //collided = true;
            if(ApplyJarateOnHit == true)
            {
                other.GetComponent<TowerDefenceAITest_V1>().CoverInJarate();
            }
            if(ApplyBulletSlowdownOnHit == true)
            {
                other.GetComponent<TowerDefenceAITest_V1>().SlowDownViaBulletSlowdown();
            }
            other.GetComponent<TowerDefenceAITest_V1>().TakeDamage(damage);
            Debug.Log("Collided with Enemy");
            var impact = Instantiate (impactVFX, collision.contacts[0].point, Quaternion.identity) as GameObject;
            Destroy(impact, 2);
            Destroy (gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
    
}
