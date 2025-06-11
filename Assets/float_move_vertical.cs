using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class float_move : MonoBehaviour
{
    Vector2 Start_Position;
    const double V = Math.PI / 180 ;
    float Angle = 100*(float)V;
    float A = 3;//êUïù
    
    void Start()
    {
        Start_Position = transform.position;
        
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(Start_Position.x, Start_Position.y + A* (float)Math.Sin(Angle*Time.time));
    }
}
