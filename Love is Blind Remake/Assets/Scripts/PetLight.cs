using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PetLight : MonoBehaviour
{
    public GameObject player;
    public GameObject petLight;
    public Light2D petLightLight;
    public PlayerController playerController;

    public GameObject petsOrigin;
    public GameObject flashingPosition;

    public Vector2 originalPosition;

    //Follow Target Variables;
    [SerializeField] private List<Vector3> storedPositions;
    [SerializeField] private int followDistance;
    bool followPlayer = true;

    public float speed;


    private void Awake()
    {
        player = this.gameObject;
        petLightLight = GetComponentInChildren<Light2D>();
        playerController = GetComponent<PlayerController>();

        storedPositions = new List<Vector3>(); //create a blank list


        if (!player)
        {
            Debug.Log("The FollowingMe gameobject was not set");
        }

        if (followDistance == 0)
        {
            Debug.Log("Please set distance higher then 0");
        }

    }

    private void Update()
    {
        Flash();

        if (followPlayer)
        {
            FollowPlayer();
        }
        
    }



    public void Flash()
    {
        if (playerController.flashing)
        {
            followPlayer = false;
            petLight.transform.position = Vector3.Lerp(petLight.transform.position,
                flashingPosition.transform.position, speed * Time.deltaTime);
            petLightLight.intensity = 1.66f;
            petLightLight.pointLightInnerRadius = 0.02f;
            petLightLight.pointLightOuterRadius = 9.05f;
            petLightLight.falloffIntensity = 0.7f;
        }
        else
        {
            petLight.transform.position = Vector3.Lerp(petLight.transform.position,
                petsOrigin.transform.position, speed * Time.deltaTime);
            petLightLight.intensity = 1f;
            petLightLight.pointLightInnerRadius = 1.72f;
            petLightLight.pointLightOuterRadius = 2.51f;
            petLightLight.falloffIntensity = 1f;
            StartCoroutine(WaitForFlashComplete());
        }
    }
    IEnumerator WaitForFlashComplete()
    {
        yield return new WaitForSeconds(0.8f);
        followPlayer = true;
    }


    public void FollowPlayer()
    {
        if (storedPositions.Count == 0)
        {
            storedPositions.Add(new Vector2(petsOrigin.transform.position.x, petsOrigin.transform.position.y)); //store the players currect position
            return;
        }
        else if (storedPositions[storedPositions.Count - 1] != petsOrigin.transform.position)
        {
            //Debug.Log("Add to list");
            storedPositions.Add(new Vector2(petsOrigin.transform.position.x, petsOrigin.transform.position.y)); //store the position every frame
        }

        if (storedPositions.Count > followDistance)
        {
            petLight.transform.position = Vector2.Lerp(petLight.transform.position, storedPositions[0], speed * Time.deltaTime); //move
            //petLight.transform.position = storedPositions[0];
            storedPositions.RemoveAt(0); //delete the position that player just move to
        }
        
    }


}

