using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public GameObject worldSpawn;
    public GameObject player;
    public PlayerController playerController;
    //public GameObject mainCamera;
    public GameObject uI;

    private void Awake()
    {
        if (GameManager._instance == null)
        {
            GameManager._instance = this;
            UnityEngine.Object.DontDestroyOnLoad(this);
            player = GameObject.Find("Player");

            uI = GameObject.Find("UI");

        }
        else
        {
            if (this != GameManager._instance)
            {
                UnityEngine.Object.Destroy(base.gameObject);
                return;
            }
            player = GameObject.Find("Player");

            uI = GameObject.Find("UI");

        }
        SceneManager.sceneLoaded += OnSceneLoaded;

    }



    void OnSceneLoaded(Scene sc, LoadSceneMode mode)
    {
        //player = GameObject.Find("Player");
        //worldSpawn = GameObject.Find("World Spawn");
        //uI = GameObject.Find("UI");
        
        
        if (SceneManager.GetActiveScene().name == "Menu" || SceneManager.GetActiveScene().name == "Game Over" || SceneManager.GetActiveScene().name == "End Title" || SceneManager.GetActiveScene().name == "Tutorial")
        {
            player.SetActive(false);
            //mainCamera.SetActive(false);
            uI.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().name != "Menu" || SceneManager.GetActiveScene().name != "Game Over" || SceneManager.GetActiveScene().name != "End Title" || SceneManager.GetActiveScene().name != "Tutorial")
        {
            worldSpawn = GameObject.Find("World Spawn");
            PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);

            //mainCamera = GameObject.Find("Main Camera");
            player.SetActive(true);
            uI.SetActive(true);
           
            playerController = player.GetComponent<PlayerController>();
        }
        if (worldSpawn)
        {
            playerController.currentSafeSpot = worldSpawn;
            player.transform.position = worldSpawn.transform.position;
        }
    }

    public void DestroyThyself()
    {
        Destroy(player);
        //Destroy(mainCamera);
        Destroy(uI);
    }

}
