using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBullet : MonoBehaviour
{
    [SerializeField] private float force;//just to control the speed
    private GameObject player;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector2 direction = player.transform.position - transform.position;
        rb.linearVelocity = new Vector2(direction.x, rb.linearVelocity.y).normalized * force;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!PlayerController.instance.isCrouching)
            {
                PlayerHealthController.instance.PlayerTakeDamage(10);
                DestroyBullet();
            }
            else
            {
                Invoke("DestroyBullet",2f);
            }
        }
    }
}
