using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HealthManager : MonoBehaviour
{

    public PlayerController player;
    private int health;

    [SerializeField] private Image[] healthImage;
    [SerializeField] private GameObject healthFull;
    [SerializeField] private GameObject health1Third;
    [SerializeField] private GameObject health2Third;
    [SerializeField] private GameObject healthEmpty;

    private void Start()
    {
        player = GetComponent<PlayerController>();
        healthFull = GameObject.Find("Canvas/Health Bar/HealthFull");
        health1Third = GameObject.Find("Canvas/Health Bar/Health1Third");
        health2Third = GameObject.Find("Canvas/Health Bar/Health2Thirds");
        healthEmpty = GameObject.Find("Canvas/Health Bar/HealthEmpty");
        health = player.numberOfLives;

    }
    private void Update()
    {
        CheckDeath();
        ControlHealth();

    }


    public void ControlHealth()
    {

        if (health == 3)
        {
            healthFull.SetActive(true);
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
        SceneManager.LoadSceneAsync("GameOverScene");
    }

    public void TakeDamage()
    {
        health -= 1;
        player.TeleportToWorldSpawn();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Death Barrier"))
        {
            TakeDamage();
        }
    }

}
