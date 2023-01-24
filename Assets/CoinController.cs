using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    GameObject player;
    private float textY, aNum;
    // Start is called before the first frame update
    void Start()
    {
        textY = transform.position.y;
        aNum = 0.00002f;
    }

    // Update is called once per frame
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
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            
            PlayerController player = other.GetComponent<PlayerController>();

            if(player != null){
                player.CoinCollected();
                Destroy(gameObject);
            }
        }
    }


}
