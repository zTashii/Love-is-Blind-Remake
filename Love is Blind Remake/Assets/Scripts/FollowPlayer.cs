using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player; // in the inspector drag the gameobject the will be following the player to this field
    public int followDistance;
    public float speed;
    private List<Vector3> storedPositions;
    public PlayerController playerController;
    public CameraController cameraController;
    public Vector2 originalPosition;


    void Awake()
    {
        storedPositions = new List<Vector3>(); //create a blank list
        //playerController = GetComponent<PlayerController>();
        //cameraController = GetComponent<CameraController>();

        if (!player)
        {
            Debug.Log("The FollowingMe gameobject was not set");
        }

        if (followDistance == 0)
        {
            Debug.Log("Please set distance higher then 0");
        }
    }

    void Start()
    {

    }

    void Update()
    {
        if (!playerController.flashing)
        {
            if (storedPositions.Count == 0)
            {
                storedPositions.Add(new Vector2(player.transform.position.x, player.transform.position.y + 0.2f)); //store the players currect position
                return;
            }
            else if (storedPositions[storedPositions.Count - 1] != player.transform.position)
            {
                //Debug.Log("Add to list");
                storedPositions.Add(new Vector2(player.transform.position.x, player.transform.position.y + 0.2f)); //store the position every frame
            }

            if (storedPositions.Count > followDistance)
            {
                //transform.position = Vector2.Lerp(storedPositions[0], storedPositions[0], speed); //move
                transform.position = storedPositions[0];
                storedPositions.RemoveAt(0); //delete the position that player just move to
            }
            originalPosition = transform.position;
        }
    }
}
 
