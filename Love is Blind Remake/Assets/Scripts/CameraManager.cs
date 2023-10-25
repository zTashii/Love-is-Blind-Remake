using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;
    public Camera panningCamera;

    [SerializeField] public GameObject panningPoint;
    [SerializeField] public GameObject[] sortedPanningPoints;

    private Vector3 velocity = Vector3.zero;
    [SerializeField] private bool isPanning;
    [SerializeField] private bool isPanComplete;




    private void Start()
    {
        isPanComplete = false;
        sortedPanningPoints = GameObject.FindGameObjectsWithTag("PanningPoint").OrderBy(go => go.name).ToArray();

        isPanning = true;
        if (isPanComplete)
        {
            mainCamera.enabled = true;
            panningCamera.enabled = false;
        }
    }
    private void Update()
    {
        if (isPanning)
        {
            PanLevel();

        }
    }


    public void PanLevel()
    {
        isPanComplete = false;
        mainCamera.enabled = false;
        panningCamera.enabled = true;
        for (int i = 0; i < sortedPanningPoints.Length; i++)
        {
            panningPoint = sortedPanningPoints[i];
            Vector3 currentCameraPosition = panningCamera.transform.position;
            Vector3 panPosition = new Vector3(panningPoint.transform.position.x, panningPoint.transform.position.y, panningPoint.transform.position.z - 10);
            Debug.Log(panningPoint + " " + panPosition);
            if (!isPanComplete)
            {


                if (i == sortedPanningPoints.Length)
                {

                    isPanning = false;
                }
                else
                {
                    //panningCamera.transform.position = Vector3.MoveTowards(currentCameraPosition, panPosition, 6 * Time.deltaTime);
                    panningCamera.transform.position = Vector3.SmoothDamp(currentCameraPosition, panPosition, ref velocity, 1f);
                    isPanning = true;
                }

                //isPanComplete = true;

            }

        }
    }

}
