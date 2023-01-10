using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    
    public PlayerController player;
    public bool isOpen;
    Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isOpen = false;
        
    }

    public void openDoor(){
        if (isOpen == false){
            isOpen = true;
            animator.SetBool("IsOpen", isOpen);
            player.setU2();
        }else if(isOpen == true){
            isOpen = false;
            animator.SetBool("IsOpen", isOpen);
            player.setU2();
        }

    }

    public void setIObool(){
        isOpen = false;
        animator.SetBool("IsOpen", isOpen);
        
    }

    public bool getIsOpen(){
        return isOpen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
