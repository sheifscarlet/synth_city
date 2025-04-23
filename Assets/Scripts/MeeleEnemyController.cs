using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleEnemyController : MonoBehaviour
{
    public float moveSpeed = 3f; 
    [SerializeField] Transform player; 
    public bool isFacingRight = true;
    [SerializeField] private Collider2D triggerArea;
    //components
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sp;
    
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Get the direction of movement
        float direction = player.position.x - transform.position.x;
        // Determining the direction of animation and reflection of the sprite
        if (direction < 0)
        {
            isFacingRight = false;
            sp.flipX = true; 
            if (triggerArea != null)
            {
                Vector2 newOffset = triggerArea.offset;
                newOffset.x = Mathf.Abs(newOffset.x) * -1; // Инвертируем значение x
                triggerArea.offset = newOffset;
            }
        }
        else if (direction > 0)
        {
            isFacingRight = true;
            sp.flipX = false;
            if (triggerArea != null)
            {
                Vector2 newOffset = triggerArea.offset;
                newOffset.x = Mathf.Abs(newOffset.x); // Возвращаем абсолютное значение x
                triggerArea.offset = newOffset;
            }
        }
        animator.SetFloat("moveSpeed",Mathf.Abs(moveSpeed));  
        
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            //Getting the direction to the player
            Vector3 targetDirection = player.position - transform.position;
            targetDirection.Normalize();
            // Moving the enemy in the direction of the player
            rb.linearVelocity = targetDirection * moveSpeed;
        }
        
    }
    
    public void Setup( Transform playerObject)
    {
        player = playerObject;
        
    }
    
    
    
    
}
