using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Enemy enemy;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        int b = enemy.getNK();
        if(b < 5 && gameObject.activeSelf == true){
            gameObject.SetActive(false);
        }
        
        
    }
    public void ActivatePortal(){
        print("The Portal Has Been Opened!!");
    }
    public void Activate(){
        gameObject.SetActive(true);
    }
}
