using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Enemy enmi;
    public float num; 

    void Start()
    {
        num = transform.localScale.x;
    }

    private void FixedUpdate(){
        float b = enmi.getHealth();
        transform.localScale = new Vector3(num * b, transform.localScale.y, transform.localScale.z);
        float a = 1.0f - b;
        transform.localPosition = new Vector2(0 - ((num + 0.01f) / 2) * a, transform.localPosition.y);
           
    }

    
}
