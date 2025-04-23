using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string newGameLevel;

    public void NewGame()
    {
        SceneManager.LoadScene(newGameLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
