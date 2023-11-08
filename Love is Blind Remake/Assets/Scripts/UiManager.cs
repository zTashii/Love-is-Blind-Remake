using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
     private static UiManager _instance;
    private void Awake()
    {
        if (UiManager._instance == null)
        {
            UiManager._instance = this;
            UnityEngine.Object.DontDestroyOnLoad(this);
        }
        else if (this != UiManager._instance)
        {
            UnityEngine.Object.Destroy(base.gameObject);
            return;
        }


    }

}
