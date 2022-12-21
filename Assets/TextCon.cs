using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextCon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponentInParent<ChestController2>().getIsOpen() == true){
            // print("The Chest is Open");
            Destroy(gameObject);
        }
        
    }
    
}
