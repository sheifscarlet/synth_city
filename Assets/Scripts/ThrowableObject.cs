using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    
    [SerializeField] private Transform throwPos;
    [SerializeField] private float speed;
    private bool canHurt = false;
    //bool for player anim
    public bool isThrowing = false;
    private bool canThrow;
    //components
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    

    
    // Start is called before the first frame update
    void Start()
    {
        
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if ((canThrow == true) && (!PlayerController.instance.isShooting) && (!PlayerController.instance.isReloading))
        {
            Throw();
        }
    }
    private void HideObject()
    {
        sr.enabled = false;
    }

    void Throw()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerController.instance.StartThrowing();
            isThrowing = true;
            StartCoroutine(DelayedThrowing());
        }
    }

    IEnumerator DelayedThrowing()
    {
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(Throwing());
    }

    IEnumerator Throwing()
    {
        transform.position = throwPos.position;
        animator.SetTrigger("flip");
        canHurt = true;
        if (PlayerController.instance.isFacingRight)
        {
            rb.linearVelocity = Vector2.right * speed;
        }
        else if (!PlayerController.instance.isFacingRight)
        {
            rb.linearVelocity = Vector2.left * speed;
        }

        yield return new WaitForSeconds(0.3f);
        PlayerController.instance.StopThrowing();
        isThrowing = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canThrow = true;
        }

        if (other.CompareTag("Enemy"))
        {
            if (canHurt)
            {
                AudioManager.instance.PlaySFX(5);
                //Destroy(other.gameObject);
                EnemyHealthController enemyHealth = other.GetComponent<EnemyHealthController>(); 
                enemyHealth.EnemyTakeDamage(1);
                canHurt = false;
                HideObject();
                Destroy(gameObject,1f);
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canThrow = false;
        }
    }
}
