using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    private Rigidbody2D rb;
    Vector2 bulletDirection;
    
    //Random Clip drop
    [SerializeField] float dropChance = 0.3f;
    private GameObject clipDrop;
    
    // Start is called before the first frame update
    void Start()
    {
        clipDrop = GameObject.FindGameObjectWithTag("Clip");
        rb = GetComponent<Rigidbody2D>();
        bulletDirection = Vector2.right;
        if (!PlayerController.instance.isFacingRight)
        {
            bulletDirection = Vector2.left;
        }
        rb.linearVelocity = bulletDirection * bulletSpeed;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealthController enemyHealth = other.GetComponent<EnemyHealthController>(); 
            enemyHealth.EnemyTakeDamage(1);
            Destroy(gameObject);
            //RandomClipDrop();
        }
    }

    void RandomClipDrop()
    {
        float randomChance = Random.Range(0f, 1f); // generate random number from 0 to 1

        // if rand num less or equal drop chance, create clip
        if (randomChance <= dropChance)
        {
            //Instantiate(medKitPrefab, transform.position, Quaternion.identity);
            GameObject clipGameObject = Instantiate(clipDrop, transform.position, Quaternion.identity);
        }
    }
}
