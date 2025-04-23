using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    //Movement
    [SerializeField] float moveSpeed;
    private float horizontal;
    //Crouch
    public  bool isCrouching;
    //Shooting
    public bool isShooting;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletPoint;
    private float timeSinceLastShot = 0.0f;
    [SerializeField] float shootCooldown = 0.3f;
    //Kick/Attack
    public bool isKicking;
    [SerializeField] Collider2D hitboxCollider;
    //Throwing
    private bool isThrowing;
    //Ammo
    [SerializeField] int maxAmmo;
    public  int currentAmmo;
    //Clips
    [SerializeField] private int maxClips;
    public int currentClips;
    //Reload
    private bool canReload;
    public bool isReloading;
    //KnockBack
    public float knockBackLength,knockBackForce;
    private float knockBackCount;
    //Components
    private Rigidbody2D rb2d;
    private Animator animator;
    public bool isFacingRight = true;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
        currentClips = maxClips;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hitboxCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isCrouching",isCrouching);
        animator.SetBool("isKicking",isKicking);
        animator.SetBool("isShooting",isShooting);
        animator.SetBool("isReloading",isReloading);
        
        if (isThrowing)
        {
            rb2d.linearVelocity = Vector2.zero;
        }
        if (knockBackCount <= 0)
        {
            if ((!isCrouching) && (!isShooting) && (!isReloading) && (!isThrowing) && (!isKicking))
            {
                Movement();
            }
            if (currentAmmo > 0 && (!isReloading) && (!isThrowing) && (!isKicking))
            {
                Shoot();
            }
            Crouch();
            Reload();
            Kick();
        }
        else
        {
            
            knockBackCount -= Time.deltaTime;
            if(isFacingRight)
            {
                rb2d.linearVelocity = new Vector2(-knockBackForce, rb2d.linearVelocity.y);
            }
            else
            {
                rb2d.linearVelocity = new Vector2(knockBackForce, rb2d.linearVelocity.y);
            }
        }

        
    }
    
    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        if (h > 0 && !isFacingRight)
            Flip();
        else if (h < 0 && isFacingRight)
            Flip();
    }

    void Flip()
    {
        if (knockBackCount <= 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        
    }
    
    private void Movement()
    {
        horizontal = Input.GetAxis("Horizontal");
        rb2d.linearVelocity = new Vector2(horizontal * moveSpeed, rb2d.linearVelocity.y);
        animator.SetFloat("speed", Mathf.Abs(horizontal));
    }
    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            rb2d.linearVelocity = Vector2.zero;
            isCrouching = !isCrouching; 
            
        }
    }

    private void Kick()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            isKicking = true;
            ActivateHitbox();
            CameraShake.instance.ShakeCamera(1f,0.1f);
            StartCoroutine(StopKicking());
        }
    }
    private IEnumerator StopKicking()
    {
        yield return new WaitForSeconds(0.3f);
        DeactivateHitbox();
        isKicking = false;
    }
    public void ActivateHitbox()
    {
        hitboxCollider.enabled = true;
        
    }

    public void DeactivateHitbox()
    {
        hitboxCollider.enabled = false;
        
    }
    
    
    public void StartThrowing() {
        isThrowing = true;
        animator.SetBool("isThrowing", true);
        
    }

    public void StopThrowing() {
        isThrowing = false;
        animator.SetBool("isThrowing", false);
        
    }
    
    
   
    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1") && currentAmmo > 0)
        {
            rb2d.linearVelocity = Vector2.zero;
            isShooting = true;
            if (Time.time - timeSinceLastShot >= shootCooldown)
            {
                timeSinceLastShot = Time.time;
                StartCoroutine(ShootBullets());
            }
            
        }
        
        if (Input.GetButtonUp("Fire1"))
        {
            isShooting = false;
        }
    }
    
    private IEnumerator ShootBullets()
    {
        while (isShooting && currentAmmo > 0 )
        {
            AudioManager.instance.PlaySFX(3);
            CameraShake.instance.ShakeCamera(2f,0.1f);
            Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
            currentAmmo--;
            if ((currentAmmo <= 0) )
            {
                AudioManager.instance.PlaySFX(1);
                isShooting = false;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo == 0 && currentClips > 0)
        {
            AudioManager.instance.PlaySFX(2);
            currentClips--;
            rb2d.linearVelocity = Vector2.zero;
            currentAmmo = maxAmmo;
            isReloading = true;
            StartCoroutine(Reloading());
        }
    }

    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(0.2f);
        isReloading = false;
    }
    
    public void KnockBack()
    {
        isShooting = false;
        isCrouching = false;
        
        knockBackCount = knockBackLength;
        rb2d.linearVelocity = new Vector2(0f, knockBackForce);
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Clip"))
        {
            currentClips++;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Enemy"))
        { 
            EnemyHealthController enemyHealth = other.GetComponent<EnemyHealthController>();
            if (isKicking)
            {
                
                enemyHealth.EnemyTakeDamage(1);
            }
            
        }
    }
    
}
