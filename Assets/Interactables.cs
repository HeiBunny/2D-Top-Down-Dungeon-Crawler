using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactables : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;

    public TextCon text;
    private bool isActive;
    
    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isInRange){
            if(Input.GetKeyDown(interactKey)){
                interactAction.Invoke();
            }
            if(isActive == false && text != null){
                text.Activate();
                isActive = true;
            }
        }
        else{
            if(isActive == true&& text != null){
                text.DeActivate();
                isActive = false;
            }
        }
        
    }
    
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            isInRange = true;
            // print("Player is in range");
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Player"){
            isInRange = false;
            // print("Player is not in range");
        }
    }
}
