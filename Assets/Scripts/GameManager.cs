using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
     
    public UnitHealth playerHealth = new UnitHealth(50, 50);
   
    private void Awake()
    {
        Instance = this;
    }
    // Sta
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
