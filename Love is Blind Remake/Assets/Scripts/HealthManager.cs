using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HealthManager : MonoBehaviour
{

    public PlayerController player;
    public int health;

    //[SerializeField] private Image[] healthImage;
    [SerializeField] private GameObject healthFull;
    [SerializeField] private GameObject health1Third;
    [SerializeField] private GameObject health2Third;
    [SerializeField] private GameObject healthEmpty;

    private void Start()
    {
        healthFull = GameObject.Find("UI/Health Bar/HealthFull");
        health1Third = GameObject.Find("UI/Health Bar/Health1Third");
        health2Third = GameObject.Find("UI/Health Bar/Health2Thirds");
        healthEmpty = GameObject.Find("UI/Health Bar/HealthEmpty");
        health = player.numberOfLives;

    }
    private void Update()
    {
        CheckDeath();
        ControlHealth();

    }
    private void Awake()
    {
        player = GetComponent<PlayerController>();

    }

    public void Heal()
    {
        if (health < player.numberOfLives)
        {
            health++;
        }
    }

    public void ControlHealth()
    {

        if (health == 3)
        {
            healthFull.SetActive(true);
            health2Third.SetActive(false);
            health1Third.SetActive(false);
            healthEmpty.SetActive(false);
        }
        else if (health == 2)
        {
            health2Third.SetActive(true);
            healthFull.SetActive(false);
        }
        else if (health == 1)
        {
            health1Third.SetActive(true);
            health2Third.SetActive(false);
        }
        else
        {
            healthEmpty.SetActive(true);
            health1Third.SetActive(false);

        }
    }

    public void CheckDeath()
    {

        if (health <= 0)
        {
            Debug.Log("Game Over");
            //teleport to end screen
            
            TeleportToEndGameScreen();

        }
    }

    public void TeleportToEndGameScreen()
    {
        SceneManager.LoadSceneAsync("Game Over");
    }

    [ContextMenu("TakeDamage")]
    public void TakeDamage()
    {
        health -= 1;
        
        if (player.currentSafeSpot)
        {
            player.TeleportToSafeSpot();
        }
        else
        {
            player.TeleportToWorldSpawn();
        }
    }

}
