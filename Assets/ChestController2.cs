using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController2 : MonoBehaviour
{
    public PlayerController player;
    public bool isOpen;
    Animator animator;

    void Start()
    {
        
        animator = GetComponent<Animator>();
        isOpen = false;

    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openChest(){
        if (isOpen == false){
            isOpen = true;
            // print("The Chest Is Open!");
            animator.SetBool("IsOpen", isOpen);
            player.setU2();
                
                System.Console.WriteLine("Wait start");
                print("hi");
                System.Threading.Thread.Sleep(5000);
                System.Console.WriteLine("Wait start");

            // ChestController2.Destroy;
            
        }

    }

    public void setIObool(){
        isOpen = false;
        animator.SetBool("IsOpen", isOpen);
        
    }

    public bool getIsOpen(){
        return isOpen;
    }
    
}
