using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject uiText;
    float timer = 2f;
    bool startTimer;

    private void Update()
    {
        if (startTimer)
        {
            timer -= Time.deltaTime;
            
        }
        if (timer <= 0)
        {

            uiText.SetActive(false);
            startTimer = false;
            Destroy(gameObject);
            Destroy(uiText);
        }
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           
            startTimer = true;
            uiText.SetActive(true);
            
        }
    }
  


}
