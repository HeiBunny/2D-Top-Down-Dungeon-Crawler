using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextCon : MonoBehaviour
{
    
    private float textY, aNum;
    void Start()
    {
        textY = transform.position.y;
        aNum = 0.00001f;
    }

    void Update()
    {
        float anothaNum = (aNum/Mathf.Abs(aNum)) * (textY + 0.03f - transform.position.y)/250;
        if(transform.position.y < (textY + 0.03) && transform.position.y > (textY - 0.03)){
            transform.position = new Vector2(transform.position.x, transform.position.y + aNum + anothaNum);
        } 
        else{
            aNum = -1 * aNum;
            anothaNum = -1 * anothaNum;
            transform.position = new Vector2(transform.position.x, transform.position.y + aNum + anothaNum);
        }
        
        if(GetComponentInParent<ChestController2>().getIsOpen() == true){
            // print("The Chest is Open");
            Destroy(gameObject);
        }
        
    }
    
}
