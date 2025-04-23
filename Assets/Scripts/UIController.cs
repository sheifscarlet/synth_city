using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]  TextMeshProUGUI currentAmmoText;
    [SerializeField] private TextMeshProUGUI currentArmorText;
    [SerializeField]  TextMeshProUGUI scoreText;
    private float score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentAmmoText.text = PlayerController.instance.currentAmmo.ToString() + ":" + PlayerController.instance.currentClips.ToString();
        currentArmorText.text = GameManager.Instance.playerHealth.Health.ToString();
        score += 1 * Time.deltaTime;
        scoreText.text = ((int)score).ToString();
    }
}
