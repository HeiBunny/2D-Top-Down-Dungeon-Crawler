using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;


public class PortalController : MonoBehaviour
{
    public Enemy enemy;
    private GameObject player, portal;
    private Scene LKnGrassScene, LKnDungScene;

    private bool isGoingToSC;

    void Start()
    {
        LKnDungScene = SceneManager.GetSceneByName("DungeonScene");
        isGoingToSC = false;
    }

    private void FixedUpdate()
    {
        int b = enemy.getNK();
        if(b < 1 && gameObject.activeSelf == true){
            gameObject.SetActive(false);
        }
        
        
    }
    public void ActivatePortal(){
        player = GameObject.FindWithTag("Player");
        portal = GameObject.FindWithTag("Portal");
        print("The Portal Has Been Opened!!");
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(portal);
        if(SceneManager.GetActiveScene().name == "SampleScene"){
            // LKnGrassScene = SceneManager.GetActiveScene();
            // AddSceneToBS();
            Scene scene = SceneManager.GetSceneByName("DungeonScene");
            // SceneManager.SaveScene(SceneManager.GetActiveScene());
            SceneManager.LoadSceneAsync("DungeonScene");
            SceneManager.MoveGameObjectToScene(player, scene);
            SceneManager.MoveGameObjectToScene(portal, scene);
        }
        else if(SceneManager.GetActiveScene().name == "DungeonScene"){
            Scene scene = SceneManager.GetSceneByName("GrassScene");
            // LKnDungScene = SceneManager.GetActiveScene();
            // AddSceneToBS();
            // SceneManager.SaveScene(SceneManager.GetActiveScene());
            isGoingToSC = true;
            SceneManager.LoadSceneAsync("GrassScene");
            // SceneManager.SetActiveScene(LKnGrassScene);
            
            
            SceneManager.MoveGameObjectToScene(player, scene);
            SceneManager.MoveGameObjectToScene(portal, scene);
            // DeleteAll2();
            
        }
        
        
        // SceneManager.SetActiveScene(SceneManager.GetSceneByName("DungeonScene"));
        
        
    
    }
    public void Activate(){
        gameObject.SetActive(true);
    }

    public void AddSceneToBS()
    {
        // Get the current active scene
        Scene currentScene = SceneManager.GetActiveScene();
        string scenePath = currentScene.path;

        // Check if the scene is already in the build settings
        bool sceneExists = false;
        foreach (EditorBuildSettingsScene s in EditorBuildSettings.scenes)
        {
            if (s.path == scenePath)
            {
                sceneExists = true;
                break;
            }
        }

        // If the scene is not in the build settings, add it
        if (!sceneExists)
        {
            EditorBuildSettingsScene newScene = new EditorBuildSettingsScene(scenePath, true);
            List<EditorBuildSettingsScene> scenes = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
            scenes.Add(newScene);
            EditorBuildSettings.scenes = scenes.ToArray();
        }
    }

    public void DeleteAll(){
        foreach (GameObject o in Object.FindObjectsOfType<GameObject>()) {
            Destroy(o);
        }
     }

    void DeleteAll2() {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects) {
            if (go.activeInHierarchy && go.tag == "Player"){
                GameObject.Destroy(go);
            }
                
        }
    
    }

    void OnEnable(){
        if(isGoingToSC == true){
            SceneManager.sceneLoaded += OnSceneLoaded;
            print("你好");
        }
        print("Hu tao");
    }

    void OnDisable(){
        if(isGoingToSC == true){
            print("ni hao");
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
        // isGoingToSC = false;

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        DeleteAll2();
    }
}
