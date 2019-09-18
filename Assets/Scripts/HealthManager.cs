using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    public static int playerHealth;
    public int maxPlayerHealth;
    public bool isDead;

    //  Text txt;
    Slider healthBar;
    LevelManager lvlManager;

   /// LifeManager lifeManager;

    void Start()
    {
        isDead = false;
        //   txt = GetComponent<Text>();
        healthBar = GetComponent<Slider>();

        playerHealth = maxPlayerHealth;
       // playerHealth = PlayerPrefs.GetInt("PlayerCurrentHealth");

        lvlManager = FindObjectOfType<LevelManager>();

   ///     lifeManager = FindObjectOfType<LifeManager>();
    }

    void Update() {

        if (playerHealth <= 0 && !isDead)
        {

            playerHealth = 0;       // so that no -ve value appears as the player health
            lvlManager.RespawnPlayer();
            isDead = true;
   ///         lifeManager.TakeLife();
        }

        //   txt.text = "" + playerHealth;
        healthBar.value = playerHealth;
    }


    public static void HurtPlayer(int damageToGive)
    {
        playerHealth -= damageToGive;
 ///       PlayerPrefs.SetInt("PlayerCurrentHealth", playerHealth);
    }

    //resetting player health
    public void FullHealth()
    {
        ///  playerHealth = PlayerPrefs.GetInt("PlayerMaxHealth");
        ///       PlayerPrefs.SetInt("PlayerCurrentHealth", playerHealth);
        playerHealth = maxPlayerHealth;
    }
}
