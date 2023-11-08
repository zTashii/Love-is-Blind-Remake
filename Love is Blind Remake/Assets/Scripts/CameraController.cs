using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraController : MonoBehaviour
{
    public GameObject followTarget;
    private Vector3 targetPosition;
    public float moveSpeed;

    private bool isPanComplete = false;
    private bool isFlashComplete = false;
    PlayerController player;

    private void Start()
    {
        isPanComplete = true;
        player = followTarget.GetComponent<PlayerController>();
        

        //PanLevel();
    }

    private void Update()
    {
        //PanLevel();
        Flash();
        if (isPanComplete || isFlashComplete)
        {
            this.targetPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y + 1f, followTarget.transform.position.z - 9f);
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, targetPosition, moveSpeed * Time.deltaTime);
           // this.transform.position = this.targetPosition;
        }

    }

    public void Flash()
    {
        if (player.flashing)
        {
            isFlashComplete = false;
            this.targetPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, followTarget.transform.position.z - 27f);
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            
        }
        else
        {
            isFlashComplete = true;
        }
    }

}
