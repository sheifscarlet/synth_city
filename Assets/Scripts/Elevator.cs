using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float speed; 
    public int startPoint; 
    public Transform[] points;
    private int currentPointIndex; 
    private bool isMoving = false; 
    
    void Start()
    {
        transform.position = points[startPoint].position;
        currentPointIndex = startPoint;
    }
    
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && !isMoving)
            {
                isMoving = true;
                if (Vector2.Distance(transform.position, points[currentPointIndex].position) < 0.02f)
                {
                    currentPointIndex = startPoint;
                }
            }
            if (isMoving)
            {
                if (Vector2.Distance(transform.position, points[currentPointIndex].position) < 0.02f)
                {
                    currentPointIndex++;
                    if (currentPointIndex == points.Length)
                    {
                        currentPointIndex = 0;
                    }
                }
                transform.position = Vector2.MoveTowards(transform.position, points[currentPointIndex].position, speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, points[currentPointIndex].position) < 0.02f)
                {
                    isMoving = false;
                }
            }
        }
    
        private void OnCollisionEnter2D(Collision2D collision)
        {
            collision.transform.SetParent(transform);
        }
    
        private void OnCollisionExit2D(Collision2D collision)
        {
            collision.transform.SetParent(null);
        }
}
