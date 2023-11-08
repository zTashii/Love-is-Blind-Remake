using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    //public PlayerController player;
    public GameObject currentDoor;
    public GameObject affectedDoor;
    public bool isActive;
    public bool canInteract;

    public SpriteRenderer spriteRenderer;

    public Sprite onSwitch;
    public Sprite offSwitch;

    [SerializeField] private GameObject interactText;

    private void Start()
    {
        //player = FindObjectOfType<PlayerController>();
        currentDoor.SetActive(true);
        spriteRenderer = GetComponent<SpriteRenderer>();
        isActive = true;

    }
    private void Update()
    {
        if (canInteract)
        {
            if (Input.GetButtonDown("Use"))
            {
                isActive = !isActive;
                currentDoor.SetActive(isActive);
                if (affectedDoor && !affectedDoor.activeInHierarchy)
                {
                    affectedDoor.SetActive(!isActive);
                    currentDoor.SetActive(isActive);
                }
               
            }
        }
        if (currentDoor.activeInHierarchy) { spriteRenderer.sprite = onSwitch; isActive = true; }
        else { spriteRenderer.sprite = offSwitch; isActive = false; }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactText.SetActive(true);
            canInteract = true;
                //currentDoor.SetActive(false);
                //if (affectedDoor)
                //{
                    
                //    affectedDoor.SetActive(true);
                //}

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       
        if (collision.gameObject.CompareTag("Player"))
        {
            interactText.SetActive(false);
            canInteract = false;
        }
    }

}
