using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Transform Player;
    private float minX=0, maxX=206;
    private void Start(){
        Player=GameObject.FindWithTag("Player").transform;
    }
    private void Update(){
        if(Player!=null)
        {
            Vector3 vitri=transform.position;
            vitri.x=Player.position.x;
            if(vitri.x<minX) vitri.x=minX;
            if(vitri.x>maxX) vitri.x=maxX;
            transform.position=vitri; 
        }
    }
}
