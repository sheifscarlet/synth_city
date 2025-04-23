using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public bool isDead = false;
    //Invincibility Frame
    public float invincibilityLenght;//prodolzhitelnost
    private float invincibilityCount;
    
    private Rigidbody2D rb;
    private Animator animator;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.K))
        {
            PlayerTakeDamage(10);
            Debug.Log(GameManager.Instance.playerHealth.Health);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerHeal(10);
            Debug.Log(GameManager.Instance.playerHealth.Health);
        }
        if(GameManager.Instance.playerHealth.Health > 0)
        {
            
            isDead = false;
        }
        else if (GameManager.Instance.playerHealth.Health == 0)
        {
            animator.SetBool("isDead",true);
            isDead = true;
            rb.linearVelocity = Vector2.zero;
            Invoke("PlayerRebirth", 1f);
            
        }
        
        if(invincibilityCount > 0)
        {
            invincibilityCount-=Time.deltaTime;
        }
    }
    
    public void PlayerTakeDamage(int dmg)
    {
        if(invincibilityCount <= 0)
        {
            AudioManager.instance.PlaySFX(4);
            CameraShake.instance.ShakeCamera(1f,0.1f);
            GameManager.Instance.playerHealth.DmgUnit(dmg);
            if (GameManager.Instance.playerHealth.Health > 0)
            {
                invincibilityCount = invincibilityLenght;
                PlayerController.instance.KnockBack();
            }

        }
    }

    private void PlayerHeal(int heal)
    {
        GameManager.Instance.playerHealth.HealUnit(heal);
    }

    void PlayerRebirth()
    {
        
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

    }
}
