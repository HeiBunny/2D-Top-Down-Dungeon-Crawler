using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VCamController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    private CinemachineVirtualCamera vcam;

    void Start()
    {
        
        vcam = GetComponent<CinemachineVirtualCamera>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void LateUpdate(){
        vcam.Follow = player;
    }
}
