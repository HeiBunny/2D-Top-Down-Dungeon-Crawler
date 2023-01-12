using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour
{
    public Enemy enemy;
    private GameObject player, portal;

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
        player = GameObject.FindWithTag("Player");
        portal = GameObject.FindWithTag("Portal");
        print("The Portal Has Been Opened!!");
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(portal);
        if(SceneManager.GetActiveScene().name == "GrassScene"){
            Scene scene = SceneManager.GetSceneByName("DungeonScene");
            SceneManager.LoadSceneAsync("DungeonScene");
            SceneManager.MoveGameObjectToScene(player, scene);
            SceneManager.MoveGameObjectToScene(portal, scene);
        }
        else if(SceneManager.GetActiveScene().name == "DungeonScene"){
            Scene scene = SceneManager.GetSceneByName("GrassScene");
            SceneManager.LoadSceneAsync("GrassScene");
            SceneManager.MoveGameObjectToScene(player, scene);
            SceneManager.MoveGameObjectToScene(portal, scene);
        }
        
        
        // SceneManager.SetActiveScene(SceneManager.GetSceneByName("DungeonScene"));
        
        
    
    }
    public void Activate(){
        gameObject.SetActive(true);
    }
}
