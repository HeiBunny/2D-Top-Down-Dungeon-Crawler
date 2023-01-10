using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("DungeonScene");
        Scene scene = SceneManager.GetSceneByName("DungeonScene");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        SceneManager.MoveGameObjectToScene(player, scene);

    }
    public void Activate(){
        gameObject.SetActive(true);
    }
}
