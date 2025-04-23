using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemyCombat : MonoBehaviour
{
    //Shooting
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletPos;
    [SerializeField] private float cooldown;
    [SerializeField] private float distanceLimit;
    [SerializeField] private float speed;
    private float distance;
    private GameObject player;
    private float timer;
    //Components
    private Rigidbody2D rb;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position,player.transform.position);
        Debug.Log(distance);
        if (distance < distanceLimit)
        {
            
            timer += Time.deltaTime;
            if (timer > cooldown)
            {
                timer = 0;
                Shoot();
            }
        }
        else
        {
            Invoke("MoveToPlayer",1.5f);
        }
        
        
    }

    void MoveToPlayer()
    {
        Vector2 direction = player.transform.position - transform.position;
        rb.linearVelocity = new Vector2(direction.x, rb.linearVelocity.y) * speed;
        if (distance == distanceLimit )
        {
            rb.linearVelocity = Vector2.zero;
        }
        
        
    }

    void Shoot()
    {
        GameObject bulletGameObject = Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
    
    
}
