using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public PlayerController player;
    public GameObject currentDoor;
    public GameObject affectedDoor;
    public bool isActive;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        currentDoor.SetActive(true);
        isActive = true;

    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag.Contains("Player"))
        {
            if (Input.GetButton("Use"))
            {
                currentDoor.SetActive(false);
                if (affectedDoor)
                {
                    affectedDoor.SetActive(true);
                }
            }

        }
    }
}
