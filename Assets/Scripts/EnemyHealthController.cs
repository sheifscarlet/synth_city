using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public static EnemyHealthController instance;
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;
    //KnockBack
    public float knockBackLength,knockBackForce;
    private float knockBackCount;
    //Components
    private Rigidbody2D rb2d;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth == 0)
        {
            Destroy(gameObject);
        }
        
    }

    public void EnemyTakeDamage(int dmg)
    {
        if(currentHealth > 0)
        {
            currentHealth -= dmg;
            Knockback();
        }
    }

    public void Knockback()
    {
        knockBackCount = knockBackLength;
        rb2d.linearVelocity = new Vector2(0f, knockBackForce);
    }
}
