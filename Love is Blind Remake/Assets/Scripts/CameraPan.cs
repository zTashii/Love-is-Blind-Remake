using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraPan : MonoBehaviour
{
    public Camera mainCamera;
    public Camera pCamera;

    private float startTime;

    public GameObject[] sortedPanningPoints;

    private bool isPanning;
    private bool isPanComplete;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 initPosition;

    void Start()
    {
        //isPanComplete = false;
        sortedPanningPoints = GameObject.FindGameObjectsWithTag("PanningPoint").OrderBy(go => go.name).ToArray();
        initPosition = new Vector3(sortedPanningPoints[0].transform.position.x, sortedPanningPoints[0].transform.position.y, sortedPanningPoints[0].transform.position.z - 18);
        //isPanning = true;
        //if (isPanComplete)
        //{
        //    mainCamera.enabled = true;
        //    pCamera.enabled = false;
        //}
        transform.position = initPosition;
        startPosition = transform.position;
        startTime = Time.time;
    }

    private void Update()
    {
        PanLevel();
    }

    public void PanLevel()
    {
        float d = (Time.time - startTime) * 15;

        for (int i = 1; i < sortedPanningPoints.Length; i++)
        {

            endPosition = sortedPanningPoints[i].transform.position;
            startPos = new Vector3(startPosition.x, startPosition.y, startPosition.z - 18);
            endPos = new Vector3(endPosition.x, endPosition.y, endPosition.z - 18);

            float distance = Vector3.Distance(startPosition, endPosition);
            transform.position = Vector3.Lerp(startPos, endPos, d / distance);
            if (gameObject.transform.position == endPos)
            {
                startPosition = transform.position;
            }


        }

        //if gameobject position is = camera final position
        // pan complete = true


    }

    //Credit: twitch/Grimprophecy
    //public class CameraPan : MonoBehaviour
    //{
    //    [SerializeField] private Transform _startTransform;
    //    [SerializeField] private Transform _endTransform;
    //    [SerializeField] private float _speed;

    //    private float _distance;
    //    private float _startTime;

    //    public void Pan()
    //    {
    //        var s = _startTransform.position;
    //        var e = _endTransform.position;
    //        _startTime = Time.time;
    //        _distance = Vector3.Distance(s, e);
    //    }

    //    private void Update()
    //    {
    //        var d = (Time.time - _startTime) * _speed;
    //        var s = _startTransform.position;
    //        var e = _endTransform.position;
    //        var p = Vector3.Lerp(s, e, d / _distance);
    //        transform.position = p;
    //    }
    //}
}
