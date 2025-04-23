using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;
    private bool isExploded = false;
    public float explosionRadius = 5.0f;

    private GameObject explosionEffect;

    
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (!isExploded) 
            {
                Destroy(other.gameObject);
                AudioManager.instance.PlaySFX(6);
                CameraShake.instance.ShakeCamera(3f,0.1f);
                isExploded = true;
                StartCoroutine(Explode());
                Destroy(explosionPrefab);
            }
        }
    }

    IEnumerator Explode()
    {
        explosionEffect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
               
                PlayerHealthController.instance.PlayerTakeDamage(10);

                
            }
            else if (collider.CompareTag("Enemy")) // tags for another objects that need to be damaged
            {
                Destroy(collider.gameObject);
            }

        }
        
        Destroy(gameObject);
        Destroy(explosionEffect, 1.0f);
        yield return null;
    }
}
